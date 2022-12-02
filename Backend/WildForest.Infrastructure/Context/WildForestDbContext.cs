using Microsoft.EntityFrameworkCore;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Weather.Entities;

namespace WildForest.Infrastructure.Context
{
    public class WildForestDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();

        public DbSet<City> Cities => Set<City>();

        public DbSet<DayWeather> DayWeather => Set<DayWeather>();

        public WildForestDbContext(DbContextOptions<WildForestDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}