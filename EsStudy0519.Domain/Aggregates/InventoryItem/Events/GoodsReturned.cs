using Sekiban.Pure.Events;

namespace EsStudy0519.Domain.Aggregates.InventoryItem.Events;

/// <summary>
/// 商品が返品されたイベント
/// </summary>
[GenerateSerializer]
public record GoodsReturned(
    Guid ItemId, 
    int Quantity, 
    string Reason, 
    DateTime ReturnDate) : IEventPayload;
