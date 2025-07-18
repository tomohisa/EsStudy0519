using EsStudy0519.Domain.ValueObjects;
using Sekiban.Pure.Aggregates;

namespace EsStudy0519.Domain.Aggregates.WeatherForecasts.Payloads;

[GenerateSerializer]
public record DeletedWeatherForecast(
    string Location,
    DateOnly Date,
    TemperatureCelsius TemperatureC,
    string Summary
) : IAggregatePayload;
