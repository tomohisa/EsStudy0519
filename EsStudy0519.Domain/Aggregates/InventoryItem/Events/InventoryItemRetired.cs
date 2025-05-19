using Sekiban.Pure.Events;

namespace EsStudy0519.Domain.Aggregates.InventoryItem.Events;

/// <summary>
/// 在庫アイテムが退役状態になったイベント（以降新規入庫予約不可。在庫がある限りは出庫は可能）
/// </summary>
[GenerateSerializer]
public record InventoryItemRetired(
    Guid ItemId, 
    DateTime RetiredDate) : IEventPayload;
