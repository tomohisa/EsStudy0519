using Sekiban.Pure.Aggregates;

namespace EsStudy0519.Domain.Aggregates.InventoryItem.Payloads;

/// <summary>
/// 予約、入庫、出庫が不可能な状態
/// </summary>
[GenerateSerializer]
public record DisabledInventoryItem(
    Guid ItemId, 
    Guid WarehouseId, 
    string ItemName) : IAggregatePayload;
