using System.Text.Json.Serialization;
using EsStudy0519.Domain.Aggregates.InventoryItem.Events;
using EsStudy0519.Domain.Aggregates.InventoryItem.Payloads;
using EsStudy0519.Domain.Aggregates.InventoryItem.Queries;
using EsStudy0519.Domain.Aggregates.WeatherForecasts.Events;
using Sekiban.Pure.Aggregates;
using Sekiban.Pure.Documents;
using Sekiban.Pure.Events;
using Sekiban.Pure.Projectors;

namespace EsStudy0519.Domain;

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(EventDocumentCommon))]
[JsonSerializable(typeof(EventDocumentCommon[]))]
[JsonSerializable(typeof(Sekiban.Pure.Aggregates.EmptyAggregatePayload))]
[JsonSerializable(typeof(Sekiban.Pure.Projectors.IMultiProjectorCommon))]
[JsonSerializable(typeof(Sekiban.Pure.Documents.PartitionKeys))]
[JsonSerializable(typeof(Sekiban.Pure.Projectors.SerializableAggregateListProjector))]
[JsonSerializable(typeof(Sekiban.Pure.Aggregates.SerializableAggregate))]
[JsonSerializable(typeof(EventDocument<EsStudy0519.Domain.Aggregates.WeatherForecasts.Events.WeatherForecastDeleted>))]
[JsonSerializable(typeof(EsStudy0519.Domain.Aggregates.WeatherForecasts.Events.WeatherForecastDeleted))]
[JsonSerializable(typeof(EventDocument<EsStudy0519.Domain.Aggregates.WeatherForecasts.Events.WeatherForecastInputted>))]
[JsonSerializable(typeof(EsStudy0519.Domain.Aggregates.WeatherForecasts.Events.WeatherForecastInputted))]
[JsonSerializable(typeof(EventDocument<EsStudy0519.Domain.Aggregates.WeatherForecasts.Events.WeatherForecastLocationUpdated>))]
[JsonSerializable(typeof(EsStudy0519.Domain.Aggregates.WeatherForecasts.Events.WeatherForecastLocationUpdated))]
[JsonSerializable(typeof(EsStudy0519.Domain.Aggregates.WeatherForecasts.Payloads.WeatherForecast))]
[JsonSerializable(typeof(EsStudy0519.Domain.Aggregates.WeatherForecasts.Payloads.DeletedWeatherForecast))]
[JsonSerializable(typeof(EsStudy0519.Domain.Projections.Count.WeatherCountMultiProjection))]
// InventoryItem イベント
[JsonSerializable(typeof(EventDocument<EsStudy0519.Domain.Aggregates.InventoryItem.Events.InventoryItemCreated>))]
[JsonSerializable(typeof(EsStudy0519.Domain.Aggregates.InventoryItem.Events.InventoryItemCreated))]
[JsonSerializable(typeof(EventDocument<EsStudy0519.Domain.Aggregates.InventoryItem.Events.GoodsReceived>))]
[JsonSerializable(typeof(EsStudy0519.Domain.Aggregates.InventoryItem.Events.GoodsReceived))]
[JsonSerializable(typeof(EventDocument<EsStudy0519.Domain.Aggregates.InventoryItem.Events.GoodsShipped>))]
[JsonSerializable(typeof(EsStudy0519.Domain.Aggregates.InventoryItem.Events.GoodsShipped))]
[JsonSerializable(typeof(EventDocument<EsStudy0519.Domain.Aggregates.InventoryItem.Events.IncomingStockReserved>))]
[JsonSerializable(typeof(EsStudy0519.Domain.Aggregates.InventoryItem.Events.IncomingStockReserved))]
[JsonSerializable(typeof(EventDocument<EsStudy0519.Domain.Aggregates.InventoryItem.Events.OutgoingStockReserved>))]
[JsonSerializable(typeof(EsStudy0519.Domain.Aggregates.InventoryItem.Events.OutgoingStockReserved))]
[JsonSerializable(typeof(EventDocument<EsStudy0519.Domain.Aggregates.InventoryItem.Events.ReservationCancelled>))]
[JsonSerializable(typeof(EsStudy0519.Domain.Aggregates.InventoryItem.Events.ReservationCancelled))]
[JsonSerializable(typeof(EventDocument<EsStudy0519.Domain.Aggregates.InventoryItem.Events.GoodsReturned>))]
[JsonSerializable(typeof(EsStudy0519.Domain.Aggregates.InventoryItem.Events.GoodsReturned))]
[JsonSerializable(typeof(EventDocument<EsStudy0519.Domain.Aggregates.InventoryItem.Events.InventoryItemRetired>))]
[JsonSerializable(typeof(EsStudy0519.Domain.Aggregates.InventoryItem.Events.InventoryItemRetired))]
[JsonSerializable(typeof(EventDocument<EsStudy0519.Domain.Aggregates.InventoryItem.Events.InventoryItemDisabled>))]
[JsonSerializable(typeof(EsStudy0519.Domain.Aggregates.InventoryItem.Events.InventoryItemDisabled))]
// InventoryItem ペイロード
[JsonSerializable(typeof(EsStudy0519.Domain.Aggregates.InventoryItem.Payloads.ActiveInventoryItem))]
[JsonSerializable(typeof(EsStudy0519.Domain.Aggregates.InventoryItem.Payloads.RetiredInventoryItem))]
[JsonSerializable(typeof(EsStudy0519.Domain.Aggregates.InventoryItem.Payloads.DisabledInventoryItem))]
// InventoryItem クエリ
[JsonSerializable(typeof(EsStudy0519.Domain.Aggregates.InventoryItem.Queries.InventoryItemListQuery))]
[JsonSerializable(typeof(EsStudy0519.Domain.Aggregates.InventoryItem.Queries.InventoryItemListQuery.ResultRecord))]
public partial class EsStudy0519DomainEventsJsonContext : JsonSerializerContext
{
}
