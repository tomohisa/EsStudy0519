using EsStudy0519.Domain.Aggregates.InventoryItem.Events;
using EsStudy0519.Domain.Aggregates.InventoryItem.Payloads;
using Sekiban.Pure.Aggregates;
using Sekiban.Pure.Events;
using Sekiban.Pure.Projectors;
using System.Collections.Generic;

namespace EsStudy0519.Domain.Aggregates.InventoryItem;

public class InventoryItemProjector : IAggregateProjector
{
    public IAggregatePayload Project(IAggregatePayload payload, IEvent ev)
        => (payload, ev.GetPayload()) switch
        {
            // 初期状態から在庫アイテム作成
            (EmptyAggregatePayload, InventoryItemCreated e) => 
                new ActiveInventoryItem(e.ItemId, e.WarehouseId, e.ItemName, 0, new Dictionary<Guid, int>(), new Dictionary<Guid, int>()),

            // ActiveInventoryItemの状態遷移
            (ActiveInventoryItem current, GoodsReceived e) =>
                current with { QuantityInStock = current.QuantityInStock + e.Quantity },
            
            (ActiveInventoryItem current, GoodsShipped e) =>
                current with { QuantityInStock = current.QuantityInStock - e.Quantity },
            
            (ActiveInventoryItem current, IncomingStockReserved e) =>
                current with { 
                    IncomingReservations = new Dictionary<Guid, int>(current.IncomingReservations) { [e.ReservationId] = e.Quantity } 
                },
            
            (ActiveInventoryItem current, OutgoingStockReserved e) =>
                current with { 
                    OutgoingReservations = new Dictionary<Guid, int>(current.OutgoingReservations) { [e.ReservationId] = e.Quantity } 
                },
            
            (ActiveInventoryItem current, ReservationCancelled e) =>
                HandleReservationCancellation(current, e),
            
            (ActiveInventoryItem current, GoodsReturned e) =>
                current with { QuantityInStock = current.QuantityInStock + e.Quantity },
            
            (ActiveInventoryItem current, InventoryItemRetired _) =>
                new RetiredInventoryItem(
                    current.ItemId, 
                    current.WarehouseId, 
                    current.ItemName, 
                    current.QuantityInStock, 
                    current.OutgoingReservations),

            // RetiredInventoryItemの状態遷移
            (RetiredInventoryItem current, GoodsShipped e) =>
                current with { QuantityInStock = current.QuantityInStock - e.Quantity },
            
            (RetiredInventoryItem current, OutgoingStockReserved e) =>
                current with { 
                    OutgoingReservations = new Dictionary<Guid, int>(current.OutgoingReservations) { [e.ReservationId] = e.Quantity } 
                },
            
            (RetiredInventoryItem current, ReservationCancelled e) => 
                current with { 
                    OutgoingReservations = new Dictionary<Guid, int>(current.OutgoingReservations.Where(kv => kv.Key != e.ReservationId).ToDictionary(kv => kv.Key, kv => kv.Value)) 
                },

            // 任意の状態からDisabledInventoryItemへの状態遷移
            (ActiveInventoryItem current, InventoryItemDisabled _) => 
                new DisabledInventoryItem(current.ItemId, current.WarehouseId, current.ItemName),
            
            (RetiredInventoryItem current, InventoryItemDisabled _) => 
                new DisabledInventoryItem(current.ItemId, current.WarehouseId, current.ItemName),
            
            _ => payload
        };

    private IAggregatePayload HandleReservationCancellation(ActiveInventoryItem current, ReservationCancelled e)
    {
        var incomingReservations = new Dictionary<Guid, int>(current.IncomingReservations);
        var outgoingReservations = new Dictionary<Guid, int>(current.OutgoingReservations);
        
        // 入庫予約から削除を試みる
        if (incomingReservations.ContainsKey(e.ReservationId))
        {
            incomingReservations.Remove(e.ReservationId);
            return current with { IncomingReservations = incomingReservations };
        }
        
        // 出庫予約から削除を試みる
        if (outgoingReservations.ContainsKey(e.ReservationId))
        {
            outgoingReservations.Remove(e.ReservationId);
            return current with { OutgoingReservations = outgoingReservations };
        }
        
        // 予約が見つからない場合は現在の状態を返す
        return current;
    }
}
