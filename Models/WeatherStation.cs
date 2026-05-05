using System.ComponentModel.DataAnnotations;

namespace Gruppe18_FullStack.Models;

public class WeatherStation
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Location { get; set; } = string.Empty;

    public double Latitude { get; set; }
    public double Longitude { get; set; }

    // One station has MANY readings
    public List<WeatherReading> WeatherReadings { get; set; } = new();
}