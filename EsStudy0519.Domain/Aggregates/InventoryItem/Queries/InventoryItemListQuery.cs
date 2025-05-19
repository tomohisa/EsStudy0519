using EsStudy0519.Domain.Aggregates.InventoryItem.Payloads;
using ResultBoxes;
using Sekiban.Pure.Aggregates;
using Sekiban.Pure.Query;
using Sekiban.Pure.Projectors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EsStudy0519.Domain.Aggregates.InventoryItem.Queries;

/// <summary>
/// 在庫アイテムの状態を表す列挙型
/// </summary>
public enum InventoryItemState
{
    Active,
    Retired,
    Disabled
}

/// <summary>
/// 在庫アイテムの一覧を取得するクエリ
/// </summary>
[GenerateSerializer]
public record InventoryItemListQuery(
    string? ItemNameFilter = null,
    Guid? WarehouseIdFilter = null,
    InventoryItemState? StateFilter = null
) : IMultiProjectionListQuery<AggregateListProjector<InventoryItemProjector>, InventoryItemListQuery, InventoryItemListQuery.ResultRecord>
{
    public static ResultBox<IEnumerable<ResultRecord>> HandleFilter(
        MultiProjectionState<AggregateListProjector<InventoryItemProjector>> projection,
        InventoryItemListQuery query,
        IQueryContext context)
    {
        // アグリゲートの一覧を取得
        var items = projection.Payload.Aggregates
            .Where(m => m.Value.GetPayload() is ActiveInventoryItem 
                || m.Value.GetPayload() is RetiredInventoryItem 
                || m.Value.GetPayload() is DisabledInventoryItem)
            .Select(m => (
                Aggregate: m.Value, 
                PayloadType: GetPayloadType(m.Value.GetPayload()),
                ItemId: GetItemId(m.Value.GetPayload()),
                ItemName: GetItemName(m.Value.GetPayload()),
                WarehouseId: GetWarehouseId(m.Value.GetPayload()),
                QuantityInStock: GetQuantityInStock(m.Value.GetPayload())
            ));

        // ItemNameFilterがある場合はフィルタリング
        if (!string.IsNullOrWhiteSpace(query.ItemNameFilter))
        {
            items = items.Where(m => 
                m.ItemName != null && 
                m.ItemName.Contains(query.ItemNameFilter, StringComparison.OrdinalIgnoreCase));
        }

        // WarehouseIdFilterがある場合はフィルタリング
        if (query.WarehouseIdFilter.HasValue)
        {
            items = items.Where(m => m.WarehouseId == query.WarehouseIdFilter.Value);
        }

        // StateFilterがある場合はフィルタリング
        if (query.StateFilter.HasValue)
        {
            items = items.Where(m => m.PayloadType == query.StateFilter.Value);
        }

        // ResultRecordに変換
        var result = items.Select(m => new ResultRecord(
            m.Aggregate.PartitionKeys.AggregateId,
            m.ItemName ?? string.Empty,
            m.WarehouseId,
            m.QuantityInStock,
            m.PayloadType.ToString()
        ));

        return result.ToResultBox();
    }

    public static ResultBox<IEnumerable<ResultRecord>> HandleSort(
        IEnumerable<ResultRecord> filteredList,
        InventoryItemListQuery query,
        IQueryContext context)
    {
        // ItemNameでソート
        return filteredList.OrderBy(m => m.ItemName).AsEnumerable().ToResultBox();
    }

    // ペイロードの型に基づいて状態を返す
    private static InventoryItemState GetPayloadType(IAggregatePayload payload) => payload switch
    {
        ActiveInventoryItem => InventoryItemState.Active,
        RetiredInventoryItem => InventoryItemState.Retired,
        DisabledInventoryItem => InventoryItemState.Disabled,
        _ => throw new InvalidOperationException("不明なペイロードタイプです")
    };

    // ペイロードからItemIdを取得
    private static Guid GetItemId(IAggregatePayload payload) => payload switch
    {
        ActiveInventoryItem active => active.ItemId,
        RetiredInventoryItem retired => retired.ItemId,
        DisabledInventoryItem disabled => disabled.ItemId,
        _ => Guid.Empty
    };

    // ペイロードからItemNameを取得
    private static string? GetItemName(IAggregatePayload payload) => payload switch
    {
        ActiveInventoryItem active => active.ItemName,
        RetiredInventoryItem retired => retired.ItemName,
        DisabledInventoryItem disabled => disabled.ItemName,
        _ => null
    };

    // ペイロードからWarehouseIdを取得
    private static Guid GetWarehouseId(IAggregatePayload payload) => payload switch
    {
        ActiveInventoryItem active => active.WarehouseId,
        RetiredInventoryItem retired => retired.WarehouseId,
        DisabledInventoryItem disabled => disabled.WarehouseId,
        _ => Guid.Empty
    };

    // ペイロードからQuantityInStockを取得
    private static int GetQuantityInStock(IAggregatePayload payload) => payload switch
    {
        ActiveInventoryItem active => active.QuantityInStock,
        RetiredInventoryItem retired => retired.QuantityInStock,
        _ => 0
    };

    /// <summary>
    /// クエリの結果レコード
    /// </summary>
    [GenerateSerializer]
    public record ResultRecord(
        Guid Id,
        string ItemName,
        Guid WarehouseId,
        int QuantityInStock,
        string State
    );
}
