using EsStudy0519.Domain.Aggregates.WeatherForecasts.Events;
using EsStudy0519.Domain.Aggregates.WeatherForecasts.Payloads;
using EsStudy0519.Domain.ValueObjects;
using Sekiban.Pure.Aggregates;
using Sekiban.Pure.Events;
using Sekiban.Pure.Projectors;

namespace EsStudy0519.Domain.Aggregates.WeatherForecasts;

public class WeatherForecastProjector : IAggregateProjector
{
    public IAggregatePayload Project(IAggregatePayload payload, IEvent ev)
        => (payload, ev.GetPayload()) switch
        {
            (EmptyAggregatePayload, WeatherForecastInputted inputted) => new WeatherForecast(inputted.Location, inputted.Date, inputted.TemperatureC, inputted.Summary),
            (WeatherForecast forecast, WeatherForecastDeleted _) => new DeletedWeatherForecast(
                forecast.Location,
                forecast.Date,
                forecast.TemperatureC,
                forecast.Summary),
            (WeatherForecast forecast, WeatherForecastLocationUpdated updated) => forecast with { Location = updated.NewLocation },
            _ => payload
        };
}
