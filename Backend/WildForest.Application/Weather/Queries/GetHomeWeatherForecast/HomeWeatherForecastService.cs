using ErrorOr;
using MapsterMapper;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Application.Weather.Common.Models;

namespace WildForest.Application.Weather.Queries.GetHomeWeatherForecast;

public sealed class HomeWeatherForecastService : IHomeWeatherForecastService
{
    private readonly IUserRepository _userRepository;
    private readonly IWeatherForecastRepository _weatherForecastRepository;
    private readonly IMapper _mapper;

    public HomeWeatherForecastService(
        IUserRepository userRepository,
        IWeatherForecastRepository weatherForecastRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _weatherForecastRepository = weatherForecastRepository;
        _mapper = mapper;
    }

    public Task<ErrorOr<WeatherForecastDto>> GetWeatherForecastsAsync(HomeWeatherForecastQuery query)
    {
        throw new NotImplementedException();
    }
    //    var userId = PersonId.Create(query.UserId);
    //    var user = await _userRepository.GetUserWithCityByIdAsync(userId);

    //    if (user is null)
    //    {
    //        return Errors.Person.NotFoundById;
    //    }

    //    var forecastDate = query.ForecastDate;

    //    var forecasts = (List<ThreeHourWeatherForecast>?)
    //        await _weatherForecastRepository.GetWeatherForecastsWithMarkByDateAsync(user.CityId, forecastDate);

    //    if (forecasts is null || forecasts.Count == 0)
    //    {
    //        return Errors.WeatherForecast.NotFound;
    //    }

    //    var forecastWithMark = forecasts.Find(x => x.WeatherMark != null!);
    //    double mediumMark = 0;

    //    if (forecastWithMark is null)
    //    {
    //        var weatherForecast = forecasts
    //            .OrderBy(x => x.ForecastTime.Value)
    //            .First();

    //        var weatherMark = CreateWeatherMark(weatherForecast);

    //        await _weatherMarkRepository.AddWeatherMarkAsync(weatherMark);
    //    }
    //    else
    //    {
    //        mediumMark = forecastWithMark.WeatherMark.MediumMark.Value;
    //    }

    //    var forecastsDto = _mapper.Map<List<WeatherForecastDto>>(forecasts);

    //    return new WeatherForecastVm(forecastsDto, mediumMark);
    //}
}