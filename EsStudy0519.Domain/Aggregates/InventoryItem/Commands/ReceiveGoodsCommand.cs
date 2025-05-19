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
/// 商品を入庫するコマンド（ActiveInventoryItemの状態でのみ実行可能）
/// </summary>
[GenerateSerializer]
public record ReceiveGoodsCommand(
    Guid AggregateId,
    int Quantity,
    DateTime ReceivedDate
) : ICommandWithHandler<ReceiveGoodsCommand, InventoryItemProjector, ActiveInventoryItem>
{
    public PartitionKeys SpecifyPartitionKeys(ReceiveGoodsCommand command) => 
        PartitionKeys.Existing<InventoryItemProjector>(command.AggregateId);

    public ResultBox<EventOrNone> Handle(ReceiveGoodsCommand command, ICommandContext<ActiveInventoryItem> context)
    {
        // 数量が正の値であることを確認
        if (command.Quantity <= 0)
        {
            return ResultBox<EventOrNone>.Error(new InvalidOperationException("入庫数量は正の値でなければなりません"));
        }

        // AggregateIdを使用してItemIdを指定（両者は同一）
        return EventOrNone.Event(new GoodsReceived(
            command.AggregateId,
            command.Quantity,
            command.ReceivedDate));
    }
}
