using WildForest.Application.Common.Interfaces.Persistence.Repositories;

namespace WildForest.Application.Common.Interfaces.Persistence.UnitOfWork;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken token = default);

    public ICityRepository CityRepository { get; }

    public ICountryRepository CountryRepository { get; }

    public IRatingRepository RatingRepository { get; }

    public IRefreshTokenRepository RefreshTokenRepository { get; }

    public IUserRepository UserRepository { get; }

    public ILanguageRepository LanguageRepository { get; }

    public IWeatherForecastRepository WeatherForecastRepository { get; }

    public ICommentRepository CommentRepository { get; }
}