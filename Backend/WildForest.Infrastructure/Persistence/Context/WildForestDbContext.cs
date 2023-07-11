using Microsoft.EntityFrameworkCore;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Clients.Admins.Entites;
using WildForest.Domain.Clients.Users.Entities;
using WildForest.Domain.Countries.Entities;
using WildForest.Domain.Tokens.Entities;
using WildForest.Domain.Weather.Entities;

namespace WildForest.Infrastructure.Persistence.Context;

public sealed class WildForestDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public DbSet<Admin> Admins => Set<Admin>();

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public DbSet<City> Cities => Set<City>();

    public DbSet<Country> Countries => Set<Country>();

    public DbSet<ThreeHourWeatherForecast> ThreeHourWeatherForecast => Set<ThreeHourWeatherForecast>();

    public WildForestDbContext(DbContextOptions<WildForestDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WildForestDbContext).Assembly);
    }
}