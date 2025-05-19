using Sekiban.Pure.Events;

namespace EsStudy0519.Domain.Aggregates.InventoryItem.Events;

/// <summary>
/// 在庫アイテムが作成されたイベント
/// </summary>
[GenerateSerializer]
public record InventoryItemCreated(
    Guid ItemId, 
    Guid WarehouseId, 
    string ItemName) : IEventPayload;
