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
/// 予約をキャンセルするコマンド（ActiveInventoryItemまたはRetiredInventoryItemの状態で実行可能）
/// </summary>
[GenerateSerializer]
public record CancelReservationCommand(
    Guid AggregateId,
    Guid ReservationId,
    DateTime CancellationDate
) : ICommandWithHandler<CancelReservationCommand, InventoryItemProjector>
{
    public PartitionKeys SpecifyPartitionKeys(CancelReservationCommand command) => 
        PartitionKeys.Existing<InventoryItemProjector>(command.AggregateId);

    public ResultBox<EventOrNone> Handle(CancelReservationCommand command, ICommandContext<IAggregatePayload> context)
    {
        // AggregateIdをItemIdとして使用（両者は同一）
        return EventOrNone.Event(new ReservationCancelled(
            command.AggregateId,
            command.ReservationId,
            command.CancellationDate));
    }
}
