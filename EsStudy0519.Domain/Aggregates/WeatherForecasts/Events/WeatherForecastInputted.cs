using EsStudy0519.Domain.ValueObjects;
using Sekiban.Pure.Events;

namespace EsStudy0519.Domain.Aggregates.WeatherForecasts.Events;

[GenerateSerializer]
public record WeatherForecastInputted(
    string Location,
    DateOnly Date,
    TemperatureCelsius TemperatureC,
    string Summary
) : IEventPayload;
