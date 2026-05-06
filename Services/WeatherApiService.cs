using System.Text.Json;
using Gruppe18_FullStack.Data;
using Gruppe18_FullStack.Models;

namespace Gruppe18_FullStack.Services;

public class WeatherApiService
{
    private readonly HttpClient _httpClient;
    private readonly ApplicationDbContext _context;

    public WeatherApiService(HttpClient httpClient, ApplicationDbContext context)
    {
        _httpClient = httpClient;
        _context = context;
    }

    public async Task ImportWeatherData()
    {
        // Horten coordinates
        double latitude = 59.27;
        double longitude = 10.41;

        string url =
            $"https://api.met.no/weatherapi/locationforecast/2.0/compact?lat={latitude.ToString(System.Globalization.CultureInfo.InvariantCulture)}&lon={longitude.ToString(System.Globalization.CultureInfo.InvariantCulture)}";

        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Gruppe18_FullStackApp/1.0");

        var response = await _httpClient.GetAsync(url);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        using JsonDocument doc = JsonDocument.Parse(json);

        var details = doc.RootElement
            .GetProperty("properties")
            .GetProperty("timeseries")[0]
            .GetProperty("data")
            .GetProperty("instant")
            .GetProperty("details");

        double temperature =
            details.GetProperty("air_temperature").GetDouble();

        double windSpeed =
            details.GetProperty("wind_speed").GetDouble();

        double humidity =
            details.GetProperty("relative_humidity").GetDouble();

        // Use first WeatherStation in database, (so it has to be atleast one weatherstation already.)
        var station = _context.WeatherStations.FirstOrDefault();

        if (station == null)
        {
            return;
        }

        var reading = new WeatherReading
        {
            Temperature = temperature,
            WindSpeed = windSpeed,
            Humidity = humidity,
            RecordedAt = DateTime.UtcNow,
            WeatherStationId = station.Id
        };

        _context.WeatherReadings.Add(reading);

        await _context.SaveChangesAsync();
    }
}