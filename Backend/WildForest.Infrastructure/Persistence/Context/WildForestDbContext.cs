using Microsoft.EntityFrameworkCore;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Countries.Entities;
using WildForest.Domain.Tokens.Entities;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Votes.Entities;
using WildForest.Domain.Weather;
using WildForest.Domain.Weather.Entities;

namespace WildForest.Infrastructure.Persistence.Context;

public sealed class WildForestDbContext : DbContext
{
    public DbSet<City> Cities => Set<City>();

    public DbSet<Country> Countries => Set<Country>();

    public DbSet<User> Users => Set<User>();

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    
    public DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();

    public DbSet<ThreeHourWeatherForecast> ThreeHourWeatherForecasts => Set<ThreeHourWeatherForecast>();

    public DbSet<Vote> Votes => Set<Vote>();

    public WildForestDbContext(DbContextOptions<WildForestDbContext> options) : base(options)
        => Database.EnsureCreated();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(WildForestDbContext).Assembly);
}