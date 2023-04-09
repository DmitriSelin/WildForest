using WildForest.Application.Weather.Common.Models;

namespace WildForest.Application.Weather.Common;

public sealed record WeatherForecastVm(List<WeatherForecastDto> WeatherForecasts, double MediumMark);