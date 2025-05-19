using Sekiban.Pure.Events;

namespace EsStudy0519.Domain.Aggregates.InventoryItem.Events;

/// <summary>
/// 在庫アイテムが無効状態になったイベント（以降予約、入庫、出庫不可能）
/// </summary>
[GenerateSerializer]
public record InventoryItemDisabled(
    Guid ItemId, 
    DateTime DisabledDate) : IEventPayload;
