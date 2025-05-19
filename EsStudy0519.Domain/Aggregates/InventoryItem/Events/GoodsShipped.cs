using Sekiban.Pure.Events;

namespace EsStudy0519.Domain.Aggregates.InventoryItem.Events;

/// <summary>
/// 商品が出庫されたイベント
/// </summary>
[GenerateSerializer]
public record GoodsShipped(
    Guid ItemId, 
    int Quantity, 
    DateTime ShippedDate) : IEventPayload;
