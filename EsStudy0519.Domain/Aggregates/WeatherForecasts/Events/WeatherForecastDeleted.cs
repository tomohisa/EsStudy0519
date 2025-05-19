using Sekiban.Pure.Events;

namespace EsStudy0519.Domain.Aggregates.WeatherForecasts.Events;

[GenerateSerializer]
public record WeatherForecastDeleted() : IEventPayload;
