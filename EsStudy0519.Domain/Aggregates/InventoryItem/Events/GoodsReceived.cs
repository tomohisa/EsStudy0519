using Sekiban.Pure.Events;

namespace EsStudy0519.Domain.Aggregates.InventoryItem.Events;

/// <summary>
/// 商品が入庫されたイベント
/// </summary>
[GenerateSerializer]
public record GoodsReceived(
    Guid ItemId, 
    int Quantity, 
    DateTime ReceivedDate) : IEventPayload;
