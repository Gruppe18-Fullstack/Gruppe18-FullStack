using Microsoft.EntityFrameworkCore;
using Gruppe18_FullStack.Models;

namespace Gruppe18_FullStack.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<WeatherStation> WeatherStations { get; set; }
    public DbSet<WeatherReading> WeatherReadings { get; set; }
}