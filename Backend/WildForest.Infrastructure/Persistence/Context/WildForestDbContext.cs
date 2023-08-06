using Microsoft.EntityFrameworkCore;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Countries.Entities;
using WildForest.Domain.Marks;
using WildForest.Domain.Tokens.Entities;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Weather;
using WildForest.Domain.Weather.Entities;

namespace WildForest.Infrastructure.Persistence.Context;

public sealed class WildForestDbContext : DbContext
{
    public DbSet<Country> Countries => Set<Country>();

    public DbSet<City> Cities => Set<City>();

    public DbSet<User> Users => Set<User>();

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    
    public DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();

    public DbSet<ThreeHourWeatherForecast> ThreeHourWeatherForecasts => Set<ThreeHourWeatherForecast>();

    public DbSet<Mark> Marks => Set<Mark>();

    public WildForestDbContext(DbContextOptions<WildForestDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(WildForestDbContext).Assembly);
}