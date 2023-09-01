using Microsoft.EntityFrameworkCore;
using WildForest.Infrastructure.Persistence.Context;
using WildForest.UnitTests.Authentication.TestUtils;
using WildForest.UnitTests.Maps.TestUtils;

namespace WildForest.UnitTests.Common;

public sealed class DbContextFactoryInMemory
{
    public static WildForestDbContext Create()
    {
        var options = new DbContextOptionsBuilder<WildForestDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new WildForestDbContext(options);
        context.Database.EnsureCreated();

        var country = CountryFactory.Create();
        var users = UserFactory.Create(country.Cities[0].Id);

        context.Countries.Add(country);
        context.Users.AddRange(users);
        context.SaveChanges();
        return context;
    }

    public static void Destroy(WildForestDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}