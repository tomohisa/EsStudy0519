using Sekiban.Pure.Events;

namespace EsStudy0519.Domain.Aggregates.InventoryItem.Events;

/// <summary>
/// 入庫予約が作成されたイベント
/// </summary>
[GenerateSerializer]
public record IncomingStockReserved(
    Guid ItemId, 
    int Quantity, 
    Guid ReservationId, 
    DateTime ReservationDate) : IEventPayload;
