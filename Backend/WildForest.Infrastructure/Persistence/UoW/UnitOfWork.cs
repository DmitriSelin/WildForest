using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Application.Common.Interfaces.Persistence.UnitOfWork;
using WildForest.Infrastructure.Persistence.Context;
using WildForest.Infrastructure.Persistence.Repositories;

namespace WildForest.Infrastructure.Persistence.UoW;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly WildForestDbContext _context;

    private ICityRepository? cityRepository;

    public ICityRepository CityRepository
        => cityRepository ??= new CityRepository(_context);

    private ICountryRepository? countryRepository;

    public ICountryRepository CountryRepository
        => countryRepository ??= new CountryRepository(_context);

    private IRatingRepository? ratingRepository;

    public IRatingRepository RatingRepository
        => ratingRepository ??= new RatingRepository(_context);

    private IRefreshTokenRepository? refreshTokenRepository;

    public IRefreshTokenRepository RefreshTokenRepository
        => refreshTokenRepository ??= new RefreshTokenRepository(_context);

    private IUserRepository? userRepository;

    public IUserRepository UserRepository
        => userRepository ??= new UserRepository(_context);

    private IWeatherForecastRepository? weatherForecastRepository;

    public IWeatherForecastRepository WeatherForecastRepository
        => weatherForecastRepository ??= new WeatherForecastRepository(_context);

    public UnitOfWork(WildForestDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return await _context.SaveChangesAsync(token);
    }
}