using System.Text.Json.Serialization;
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
public partial class EsStudy0519DomainEventsJsonContext : JsonSerializerContext
{
}