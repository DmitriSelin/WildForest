using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Infrastructure.Persistence.Context;

namespace WildForest.Infrastructure.Persistence.Repositories;

public sealed class WeatherForecastRepository : IWeatherForecastRepository
{
    private readonly WildForestDbContext _context;

    public WeatherForecastRepository(WildForestDbContext context)
    {
        _context = context;
    }
}