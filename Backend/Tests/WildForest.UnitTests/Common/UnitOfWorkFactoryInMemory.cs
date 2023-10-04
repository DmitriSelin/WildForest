using Microsoft.EntityFrameworkCore;
using WildForest.Domain.Languages.Entities;
using WildForest.Infrastructure.Persistence.Context;
using WildForest.Infrastructure.Persistence.UoW;
using WildForest.UnitTests.Authentication.TestUtils;
using WildForest.UnitTests.Maps.TestUtils;

namespace WildForest.UnitTests.Common;

public sealed class UnitOfWorkFactoryInMemory
{
    public static UnitOfWork Create()
    {
        var options = new DbContextOptionsBuilder<WildForestDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new WildForestDbContext(options);
        context.Database.EnsureCreated();

        var country = CountryFactory.Create();
        var language = Language.Create("English");
        var users = UserFactory.Create(country.Cities[0].Id, language.Id);

        context.Countries.Add(country);
        context.Languages.Add(language);
        context.Users.AddRange(users);
        
        var unitOfWork = new UnitOfWork(context);
        RefreshTokenHelper.Initialize(users, unitOfWork);

        context.SaveChanges();
        return unitOfWork;
    }

    public static void Destroy(UnitOfWork unitOfWork)
    {
        unitOfWork.Context.Database.EnsureDeleted();
        unitOfWork.Context.Dispose();
    }
}