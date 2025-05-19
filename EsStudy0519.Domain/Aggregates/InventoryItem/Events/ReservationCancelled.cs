using Sekiban.Pure.Events;

namespace EsStudy0519.Domain.Aggregates.InventoryItem.Events;

/// <summary>
/// 予約がキャンセルされたイベント
/// </summary>
[GenerateSerializer]
public record ReservationCancelled(
    Guid ItemId, 
    Guid ReservationId, 
    DateTime CancellationDate) : IEventPayload;
