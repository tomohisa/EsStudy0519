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
/// 在庫アイテムを無効状態にするコマンド（任意の状態から実行可能）
/// </summary>
[GenerateSerializer]
public record DisableInventoryItemCommand(
    Guid AggregateId,
    DateTime DisabledDate
) : ICommandWithHandler<DisableInventoryItemCommand, InventoryItemProjector>
{
    public PartitionKeys SpecifyPartitionKeys(DisableInventoryItemCommand command) => 
        PartitionKeys.Existing<InventoryItemProjector>(command.AggregateId);

    public ResultBox<EventOrNone> Handle(DisableInventoryItemCommand command, ICommandContext<IAggregatePayload> context)
    {
        // AggregateIdをItemIdとして使用（両者は同一）
        return EventOrNone.Event(new InventoryItemDisabled(
            command.AggregateId,
            command.DisabledDate));
    }
}
