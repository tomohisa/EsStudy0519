using Sekiban.Pure.Aggregates;
using System.Collections.Generic;

namespace EsStudy0519.Domain.Aggregates.InventoryItem.Payloads;

/// <summary>
/// 入庫予約、出庫予約、入庫、出庫が可能な在庫アイテムの状態
/// </summary>
[GenerateSerializer]
public record ActiveInventoryItem(
    Guid ItemId, 
    Guid WarehouseId, 
    string ItemName, 
    int QuantityInStock, 
    Dictionary<Guid, int> IncomingReservations,
    Dictionary<Guid, int> OutgoingReservations) : IAggregatePayload;
