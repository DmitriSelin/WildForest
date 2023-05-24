using System.Collections.Generic;
using WildForest.Frontend.Contracts.Weather.Models;

namespace WildForest.Frontend.Contracts.Weather;

public sealed record WeatherForecastVm(List<WeatherForecastDto> WeatherForecasts, double MediumMark);