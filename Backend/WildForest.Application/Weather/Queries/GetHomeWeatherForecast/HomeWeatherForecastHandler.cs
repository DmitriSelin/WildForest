using ErrorOr;
using MapsterMapper;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Application.Weather.Common;
using WildForest.Domain.Common.Errors;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Domain.Weather.Entities;
using WildForest.Domain.Weather.ValueObjects;

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

    public async Task<ErrorOr<List<WeatherForecastDto>>> GetWeatherForecastAsync(HomeWeatherForecastQuery query)
    {
        var userId = UserId.Create(query.UserId);
        var user = await _userRepository.GetUserWithCityByIdAsync(userId);

        if (user is null)
        {
            return Errors.User.NotFoundById;
        }
        
        var forecastDate = ForecastDate.Create(query.ForecastDate);
        
        var forecasts = (List<WeatherForecast>?)
            await _weatherForecastRepository.GetWeatherForecastsByDateAsync(user.CityId, forecastDate);

        if (forecasts is null)
        {
            return Errors.WeatherForecast.NotFound;
        }

        var forecastsDto = _mapper.Map<List<WeatherForecastDto>>(forecasts);

        return forecastsDto;
    }
}