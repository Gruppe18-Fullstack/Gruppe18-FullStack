using Gruppe18_FullStack.Controllers;
using Gruppe18_FullStack.Data;
using Gruppe18_FullStack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Gruppe18_FullStack.Tests;

public class WeatherStationsControllerTests
{
    private ApplicationDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task Create_AddsWeatherStation()
    {
        // Arrange
        var context = GetDbContext();
        var controller = new WeatherStationsController(context);

        var station = new WeatherStation
        {
            Name = "Test Station",
            Location = "Horten",
            Latitude = 59,
            Longitude = 10
        };

        // Act
        await controller.Create(station);

        // Assert
        Assert.Single(context.WeatherStations);
    }

    [Fact]
    public async Task Index_ReturnsViewResult()
    {
        // Arrange
        var context = GetDbContext();
        var controller = new WeatherStationsController(context);

        // Act
        var result = await controller.Index();

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public async Task Delete_RemovesWeatherStation()
    {
        // Arrange
        var context = GetDbContext();

        var station = new WeatherStation
        {
            Name = "Delete Test",
            Location = "Horten",
            Latitude = 59,
            Longitude = 10
        };

        context.WeatherStations.Add(station);
        await context.SaveChangesAsync();

        var controller = new WeatherStationsController(context);

        // Act
        await controller.DeleteConfirmed(station.Id);

        // Assert
        Assert.Empty(context.WeatherStations);
    }
}