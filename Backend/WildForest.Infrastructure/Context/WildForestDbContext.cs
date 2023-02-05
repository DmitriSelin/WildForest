using Microsoft.EntityFrameworkCore;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Countries.Entities;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Weather.Entities;
using WildForest.Infrastructure.Persistence.Configurations;

namespace WildForest.Infrastructure.Context
{
    public sealed class WildForestDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();

        public DbSet<City> Cities => Set<City>();

        public DbSet<Country> Countries => Set<Country>();  

        public DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();

        public WildForestDbContext(DbContextOptions<WildForestDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CityConfiguration());
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new WeatherForecastConfiguration());
        }
    }
}