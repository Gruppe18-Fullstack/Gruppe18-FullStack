using Microsoft.EntityFrameworkCore;
using Gruppe18_FullStack.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Gruppe18_FullStack.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<WeatherStation> WeatherStations { get; set; }
    public DbSet<WeatherReading> WeatherReadings { get; set; }
}