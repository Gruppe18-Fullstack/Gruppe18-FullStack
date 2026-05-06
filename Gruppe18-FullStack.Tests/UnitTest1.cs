using Gruppe18_FullStack.Models;
using Xunit;

namespace Gruppe18_FullStack.Tests;

public class WeatherReadingTests
{
    [Fact]
    public void WeatherReading_ShouldStoreTemperature()
    {
        // Arrange
        var reading = new WeatherReading
        {
            Temperature = 12.5
        };

        // Assert
        Assert.Equal(12.5, reading.Temperature);
    }

    [Fact]
    public void WeatherReading_ShouldHaveWeatherStationId()
    {
        // Arrange
        var reading = new WeatherReading
        {
            WeatherStationId = 1
        };

        // Assert
        Assert.Equal(1, reading.WeatherStationId);
    }
}