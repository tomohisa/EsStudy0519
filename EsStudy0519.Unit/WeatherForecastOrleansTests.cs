using EsStudy0519.Domain;
using EsStudy0519.Domain.Aggregates.WeatherForecasts;
using EsStudy0519.Domain.Aggregates.WeatherForecasts.Commands;
using EsStudy0519.Domain.Aggregates.WeatherForecasts.Payloads;
using EsStudy0519.Domain.Generated;
using EsStudy0519.Domain.ValueObjects;
using ResultBoxes;
using Sekiban.Pure;
using Sekiban.Pure.Orleans.xUnit;
using Sekiban.Pure.Projectors;

namespace EsStudy0519.Unit;

public class WeatherForecastOrleansTests : SekibanOrleansTestBase<WeatherForecastOrleansTests>
{
    public override SekibanDomainTypes GetDomainTypes() => 
        EsStudy0519DomainDomainTypes.Generate(EsStudy0519DomainEventsJsonContext.Default.Options);

    [Fact]
    public void OrleansTest_CreateAndQueryWeatherForecast() =>
        GivenCommandWithResult(new InputWeatherForecastCommand(
                "Chicago", 
                new DateOnly(2025, 3, 7), 
                new TemperatureCelsius(10), 
                "Windy"))
            .Do(response => Assert.Equal(1, response.Version))
            .Conveyor(response => WhenCommandWithResult(
                new UpdateWeatherForecastLocationCommand(response.PartitionKeys.AggregateId, "New Chicago")))
            .Do(response => Assert.Equal(2, response.Version))
            .Conveyor(response => ThenGetAggregateWithResult<WeatherForecastProjector>(response.PartitionKeys))
            .Conveyor(aggregate => aggregate.Payload.ToResultBox().Cast<WeatherForecast>())
            .Do(forecast => 
            {
                Assert.Equal("New Chicago", forecast.Location);
                Assert.Equal(new DateOnly(2025, 3, 7), forecast.Date);
                Assert.Equal(new TemperatureCelsius(10), forecast.TemperatureC);
                Assert.Equal("Windy", forecast.Summary);
            })
            .Conveyor(_ => ThenGetMultiProjectorWithResult<AggregateListProjector<WeatherForecastProjector>>())
            .Do(projector => 
            {
                var aggregates = projector.Aggregates.Values;
                Assert.Single(aggregates);
                var forecast = (WeatherForecast)aggregates.First().Payload;
                Assert.Equal("New Chicago", forecast.Location);
            })
            .UnwrapBox();
            
    [Fact]
    public void TestSerializable()
    {
        // Test that commands are serializable (important for Orleans)
        CheckSerializability(new InputWeatherForecastCommand(
            "Seattle", 
            new DateOnly(2025, 3, 4), 
            new TemperatureCelsius(15), 
            "Rainy"));
            
        CheckSerializability(new UpdateWeatherForecastLocationCommand(
            Guid.NewGuid(), 
            "Portland"));
            
        CheckSerializability(new DeleteWeatherForecastCommand(
            Guid.NewGuid()));
    }
}
