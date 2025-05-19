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
/// 出庫予約を作成するコマンド（ActiveInventoryItemまたはRetiredInventoryItemの状態で実行可能）
/// </summary>
[GenerateSerializer]
public record ReserveOutgoingStockCommand(
    Guid AggregateId,
    int Quantity,
    Guid ReservationId,
    DateTime ReservationDate
) : ICommandWithHandler<ReserveOutgoingStockCommand, InventoryItemProjector>
{
    public PartitionKeys SpecifyPartitionKeys(ReserveOutgoingStockCommand command) => 
        PartitionKeys.Existing<InventoryItemProjector>(command.AggregateId);

    public ResultBox<EventOrNone> Handle(ReserveOutgoingStockCommand command, ICommandContext<IAggregatePayload> context)
    {
        // 数量が正の値であることを確認
        if (command.Quantity <= 0)
        {
            return ResultBox<EventOrNone>.Error(new InvalidOperationException("予約数量は正の値でなければなりません"));
        }

        // AggregateIdをItemIdとして使用（両者は同一）
        return EventOrNone.Event(new OutgoingStockReserved(
            command.AggregateId,
            command.Quantity,
            command.ReservationId,
            command.ReservationDate));
    }
}
