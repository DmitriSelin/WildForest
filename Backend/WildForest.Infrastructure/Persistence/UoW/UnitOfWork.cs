using Microsoft.Extensions.DependencyInjection;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Application.Common.Interfaces.Persistence.UnitOfWork;
using WildForest.Infrastructure.Persistence.Context;

namespace WildForest.Infrastructure.Persistence.UoW;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly WildForestDbContext _context;
    private readonly IServiceProvider _serviceProvider;

    private ICityRepository? cityRepository;

    public ICityRepository CityRepository
        => cityRepository ??= _serviceProvider.GetService<ICityRepository>()!;

    private ICountryRepository? countryRepository;

    public ICountryRepository CountryRepository
        => countryRepository ??= _serviceProvider.GetService<ICountryRepository>()!;

    private IRatingRepository? ratingRepository;

    public IRatingRepository RatingRepository
        => ratingRepository ??= _serviceProvider.GetService<IRatingRepository>()!;

    private IRefreshTokenRepository? refreshTokenRepository;

    public IRefreshTokenRepository RefreshTokenRepository
        => refreshTokenRepository ??= _serviceProvider.GetService<IRefreshTokenRepository>()!;

    private IUserRepository? userRepository;

    public IUserRepository UserRepository
        => userRepository ??= _serviceProvider.GetService<IUserRepository>()!;

    private IWeatherForecastRepository? weatherForecastRepository;

    public IWeatherForecastRepository WeatherForecastRepository
        => weatherForecastRepository ??= _serviceProvider.GetService<IWeatherForecastRepository>()!;

    public UnitOfWork(WildForestDbContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
    }

    public async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return await _context.SaveChangesAsync(token);
    }
}