using Sekiban.Pure.Events;

namespace EsStudy0519.Domain.Aggregates.InventoryItem.Events;

/// <summary>
/// 出庫予約が作成されたイベント
/// </summary>
[GenerateSerializer]
public record OutgoingStockReserved(
    Guid ItemId, 
    int Quantity, 
    Guid ReservationId, 
    DateTime ReservationDate) : IEventPayload;
