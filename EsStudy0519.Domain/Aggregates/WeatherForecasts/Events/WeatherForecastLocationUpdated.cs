using Sekiban.Pure.Events;

namespace EsStudy0519.Domain.Aggregates.WeatherForecasts.Events;

[GenerateSerializer]
public record WeatherForecastLocationUpdated(string NewLocation) : IEventPayload;
