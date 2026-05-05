using System.ComponentModel.DataAnnotations;

namespace Gruppe18_FullStack.Models;

public class WeatherReading
{
    public int Id { get; set; }

    [Required]
    public double Temperature { get; set; }

    public double WindSpeed { get; set; }
    public double Humidity { get; set; }

    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

    // Foreign key — links this reading to its station
    public int WeatherStationId { get; set; }
    public WeatherStation? WeatherStation { get; set; }
}