using ErrorOr;
using MapsterMapper;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Application.Weather.Common;

namespace WildForest.Application.Weather.Queries.GetHomeWeatherForecast;

public sealed class HomeWeatherForecastHandler : IHomeWeatherForecastHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IWeatherForecastRepository _weatherForecastRepository;
    private readonly IMapper _mapper;

    public HomeWeatherForecastHandler(
        IUserRepository userRepository,
        IWeatherForecastRepository weatherForecastRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _weatherForecastRepository = weatherForecastRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<WeatherForecastVm>> GetWeatherForecastAsync(HomeWeatherForecastQuery query)
    {
        throw new();
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

    //private static WeatherMark CreateWeatherMark(ThreeHourWeatherForecast forecast)
    //{
    //    var mark = MediumMark.CreateNewProperty();
    //    var countOfMarks = CountOfMarks.Create();

    //    return WeatherMark.Create(mark, countOfMarks, forecast.Id);
    //}
}