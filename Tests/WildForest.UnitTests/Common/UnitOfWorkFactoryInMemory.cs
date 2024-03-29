using Microsoft.EntityFrameworkCore;
using WildForest.Domain.Languages.Entities;
using WildForest.Infrastructure.Persistence.Context;
using WildForest.Infrastructure.Persistence.UoW;
using WildForest.UnitTests.Authentication.TestUtils;
using WildForest.UnitTests.Maps.TestUtils;
using WildForest.UnitTests.Ratings.TestUtils;
using WildForest.UnitTests.Weather.TestUtils;

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
        var weatherForecast = WeatherFabric.Create(country.Cities[0].Id);
        var rating = RatingFactory.Create(weatherForecast.Id, users.First().Id, users.Last().Id);

        context.Countries.Add(country);
        context.Languages.Add(language);
        context.Users.AddRange(users);
        context.WeatherForecasts.Add(weatherForecast);
        context.Ratings.Add(rating);
        
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