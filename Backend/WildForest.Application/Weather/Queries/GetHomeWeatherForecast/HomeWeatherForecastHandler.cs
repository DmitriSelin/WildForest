using ErrorOr;
using MapsterMapper;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Application.Weather.Common;
using WildForest.Application.Weather.Common.Models;
using WildForest.Domain.Common.Errors;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Domain.Weather.Entities;
using WildForest.Domain.Weather.ValueObjects;
using WildForest.Domain.WeatherMarks.Entities;
using WildForest.Domain.WeatherMarks.ValueObjects;

namespace WildForest.Application.Weather.Queries.GetHomeWeatherForecast;

public sealed class HomeWeatherForecastHandler : IHomeWeatherForecastHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IWeatherForecastRepository _weatherForecastRepository;
    private readonly IMapper _mapper;
    private readonly IWeatherMarkRepository _weatherMarkRepository;

    public HomeWeatherForecastHandler(
        IUserRepository userRepository,
        IWeatherForecastRepository weatherForecastRepository,
        IWeatherMarkRepository weatherMarkRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _weatherForecastRepository = weatherForecastRepository;
        _weatherMarkRepository = weatherMarkRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<WeatherForecastVm>> GetWeatherForecastAsync(HomeWeatherForecastQuery query)
    {
        var userId = UserId.Create(query.UserId);
        var user = await _userRepository.GetUserWithCityByIdAsync(userId);

        if (user is null)
        {
            return Errors.User.NotFoundById;
        }

        var forecastDate = ForecastDate.Create(query.ForecastDate);

        var forecasts = (List<WeatherForecast>?)
            await _weatherForecastRepository.GetWeatherForecastsWithMarkByDateAsync(user.CityId, forecastDate);

        if (forecasts is null || forecasts.Count == 0)
        {
            return Errors.WeatherForecast.NotFound;
        }

        var forecastWithMark = forecasts.Find(x => x.WeatherMark != null!);
        double mediumMark = 0;

        if (forecastWithMark is null)
        {
            var weatherForecast = forecasts.First(x => x.ForecastTime.Value == TimeOnly.Parse("00:00"));
            var weatherMark = CreateWeatherMark(weatherForecast);

            await _weatherMarkRepository.AddWeatherMarkAsync(weatherMark);
        }
        else
        {
            mediumMark = forecastWithMark.WeatherMark.MediumMark.Value;
        }

        var forecastsDto = _mapper.Map<List<WeatherForecastDto>>(forecasts);

        return new WeatherForecastVm(forecastsDto, mediumMark);
    }

    private static WeatherMark CreateWeatherMark(WeatherForecast forecast)
    {
        var mark = MediumMark.CreateNewProperty();
        var countOfMarks = CountOfMarks.Create();

        return WeatherMark.Create(mark, countOfMarks, forecast.Id);
    }
}