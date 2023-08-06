using MapsterMapper;
using WildForest.Application.Weather.Common.Models;
using WildForest.Domain.Weather;

namespace WildForest.Application.Weather.Queries.GetHomeWeatherForecast.Fabrics;

public sealed class WeatherForecastResponseFactory : IWeatherForecastResponseFactory
{
    private readonly IMapper _mapper;

    public WeatherForecastResponseFactory(IMapper mapper)
    {
        _mapper = mapper;
    }

    public List<WeatherForecastResponse> Create(List<WeatherForecast> forecasts)
    {
        List<WeatherForecastResponse> responses = new();

        foreach (var forecast in forecasts)
        {
            var weatherForecasts = _mapper.Map<List<WeatherForecastDto>>(forecast.ThreeHourWeatherForecasts);
            var response = CreateWeatherForecastResponse(forecast, weatherForecasts);
            responses.Add(response);
        }

        return responses;
    }

    private static WeatherForecastResponse CreateWeatherForecastResponse(
        WeatherForecast forecast, List<WeatherForecastDto> weatherForecasts)
    {
        return new(
            forecast.Id.Value, forecast.Date, weatherForecasts,
            forecast.Mark.Id.Value, forecast.Mark.Points);
    }
}