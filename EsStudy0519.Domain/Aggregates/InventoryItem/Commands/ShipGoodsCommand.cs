using EsStudy0519.Domain.Aggregates.InventoryItem.Events;
using EsStudy0519.Domain.Aggregates.InventoryItem.Payloads;
using ResultBoxes;
using Sekiban.Pure.Aggregates;
using Sekiban.Pure.Command.Executor;
using Sekiban.Pure.Command.Handlers;
using Sekiban.Pure.Documents;
using Sekiban.Pure.Events;
using System;

namespace EsStudy0519.Domain.Aggregates.InventoryItem.Commands;

/// <summary>
/// 商品を出庫するコマンド（ActiveInventoryItemまたはRetiredInventoryItemの状態で実行可能）
/// </summary>
[GenerateSerializer]
public record ShipGoodsCommand(
    Guid AggregateId,
    int Quantity,
    DateTime ShippedDate
) : ICommandWithHandler<ShipGoodsCommand, InventoryItemProjector>
{
    public PartitionKeys SpecifyPartitionKeys(ShipGoodsCommand command) => 
        PartitionKeys.Existing<InventoryItemProjector>(command.AggregateId);

    public ResultBox<EventOrNone> Handle(ShipGoodsCommand command, ICommandContext<IAggregatePayload> context)
    {
        // 数量が正の値であることを確認
        if (command.Quantity <= 0)
        {
            return ResultBox<EventOrNone>.Error(new InvalidOperationException("出庫数量は正の値でなければなりません"));
        }

        // AggregateIdをItemIdとして使用する（両者は同一）
        // 出庫数量チェック（ここではシンプルに実装）
        return EventOrNone.Event(new GoodsShipped(
            command.AggregateId,
            command.Quantity,
            command.ShippedDate));
    }

    private ResultBox<EventOrNone> HandleActiveState(ShipGoodsCommand command, ActiveInventoryItem active)
    {
        // 出庫予約分を含めた利用可能在庫のチェック
        int reservedQuantity = active.OutgoingReservations.Values.Sum();
        if (active.QuantityInStock - reservedQuantity < command.Quantity)
        {
            return ResultBox<EventOrNone>.Error(new InvalidOperationException(
                $"出庫数量が利用可能在庫を超えています。利用可能在庫: {active.QuantityInStock - reservedQuantity}, 要求数量: {command.Quantity}"));
        }

        return EventOrNone.Event(new GoodsShipped(
            active.ItemId,
            command.Quantity,
            command.ShippedDate));
    }

    private ResultBox<EventOrNone> HandleRetiredState(ShipGoodsCommand command, RetiredInventoryItem retired)
    {
        // 出庫予約分を含めた利用可能在庫のチェック
        int reservedQuantity = retired.OutgoingReservations.Values.Sum();
        if (retired.QuantityInStock - reservedQuantity < command.Quantity)
        {
            return ResultBox<EventOrNone>.Error(new InvalidOperationException(
                $"出庫数量が利用可能在庫を超えています。利用可能在庫: {retired.QuantityInStock - reservedQuantity}, 要求数量: {command.Quantity}"));
        }

        return EventOrNone.Event(new GoodsShipped(
            retired.ItemId,
            command.Quantity,
            command.ShippedDate));
    }
}
