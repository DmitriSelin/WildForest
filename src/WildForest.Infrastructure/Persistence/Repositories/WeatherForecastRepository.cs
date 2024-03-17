using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather;
using WildForest.Domain.Weather.ValueObjects;
using WildForest.Infrastructure.Persistence.Context;
using WildForest.Infrastructure.Persistence.Repositories.Base;

namespace WildForest.Infrastructure.Persistence.Repositories;

public sealed class WeatherForecastRepository : Repository<WeatherForecast>, IWeatherForecastRepository
{
    public WeatherForecastRepository(WildForestDbContext context) : base(context) { }

    public async Task AddWeatherForecastsAsync(IEnumerable<WeatherForecast> weatherForecasts)
    {
        await Context.WeatherForecasts.AddRangeAsync(weatherForecasts);
    }

    public async Task<WeatherForecast?> GetWeatherForecastByIdAsync(WeatherForecastId weatherForecastId)
    {
        return await Context.WeatherForecasts
            .FirstOrDefaultAsync(x => x.Id == weatherForecastId);
    }

    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecastsWithRatingByDateAsync(DateOnly date, CityId cityId)
    {
        return await Context.WeatherForecasts
            .Include(x => x.Rating)
            .Include(x => x.ThreeHourWeatherForecasts)
            .Where(x => x.Date >= date && x.CityId == cityId)
            .ToListAsync();
    }
}