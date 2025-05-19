using Sekiban.Pure.Aggregates;
using System.Collections.Generic;

namespace EsStudy0519.Domain.Aggregates.InventoryItem.Payloads;

/// <summary>
/// 新規の入庫予約は不可。在庫がある限り出庫予約、出庫は可能な状態
/// </summary>
[GenerateSerializer]
public record RetiredInventoryItem(
    Guid ItemId, 
    Guid WarehouseId, 
    string ItemName, 
    int QuantityInStock, 
    Dictionary<Guid, int> OutgoingReservations) : IAggregatePayload;
