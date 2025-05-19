using EsStudy0519.Domain.Aggregates.InventoryItem.Events;
using ResultBoxes;
using Sekiban.Pure.Aggregates;
using Sekiban.Pure.Command.Executor;
using Sekiban.Pure.Command.Handlers;
using Sekiban.Pure.Documents;
using Sekiban.Pure.Events;
using System;

namespace EsStudy0519.Domain.Aggregates.InventoryItem.Commands;

/// <summary>
/// 在庫アイテムを作成するコマンド
/// </summary>
[GenerateSerializer]
public record CreateInventoryItemCommand(
    Guid ItemId, 
    Guid WarehouseId, 
    string ItemName) : ICommandWithHandler<CreateInventoryItemCommand, InventoryItemProjector>
{
    public PartitionKeys SpecifyPartitionKeys(CreateInventoryItemCommand command) => 
        PartitionKeys.Generate<InventoryItemProjector>();

    public ResultBox<EventOrNone> Handle(CreateInventoryItemCommand command, ICommandContext<IAggregatePayload> context)
    {
        // 商品名の検証
        if (string.IsNullOrWhiteSpace(command.ItemName))
        {
            return ResultBox<EventOrNone>.Error(new InvalidOperationException("商品名は必須です"));
        }

        // イベント生成
        return EventOrNone.Event(new InventoryItemCreated(
            command.ItemId, 
            command.WarehouseId, 
            command.ItemName));
    }
}
