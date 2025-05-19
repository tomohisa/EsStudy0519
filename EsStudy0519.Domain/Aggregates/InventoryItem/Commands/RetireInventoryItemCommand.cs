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
/// 在庫アイテムを退役状態にするコマンド（ActiveInventoryItemの状態でのみ実行可能）
/// </summary>
[GenerateSerializer]
public record RetireInventoryItemCommand(
    Guid AggregateId,
    DateTime RetiredDate
) : ICommandWithHandler<RetireInventoryItemCommand, InventoryItemProjector, ActiveInventoryItem>
{
    public PartitionKeys SpecifyPartitionKeys(RetireInventoryItemCommand command) => 
        PartitionKeys.Existing<InventoryItemProjector>(command.AggregateId);

    public ResultBox<EventOrNone> Handle(RetireInventoryItemCommand command, ICommandContext<ActiveInventoryItem> context)
    {
        // AggregateIdをItemIdとして使用（両者は同一）
        return EventOrNone.Event(new InventoryItemRetired(
            command.AggregateId,
            command.RetiredDate));
    }
}
