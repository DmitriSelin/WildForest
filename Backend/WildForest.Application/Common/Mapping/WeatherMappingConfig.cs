using Mapster;
using WildForest.Application.Weather.Common;
using WildForest.Application.Weather.Common.Models;
using WildForest.Domain.Weather.Entities;

namespace WildForest.Application.Common.Mapping
{
    public sealed class WeatherMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<WeatherForecast, WeatherForecastDto>()
                .Map(dest => dest.WeatherId, source => source.Id.Value)
                .Map(dest => dest.Date, source => source.ForecastDate.Value)
                .Map(dest => dest.Time, source => source.ForecastTime.Value)
                .Map(dest => dest.Temperature, source => new Temperature(
                    source.Temperature.Value, source.Temperature.ValueFeelsLike))
                .Map(dest => dest.Pressure, source => source.Pressure.Value)
                .Map(dest => dest.Humidity, source => source.Humidity.Value)
                .Map(dest => dest.WeatherDescription, source => new WeatherDescription(
                    source.WeatherDescription.Name, source.WeatherDescription.Description))
                .Map(dest => dest.Cloudiness, source => source.Cloudiness.Value)
                .Map(dest => dest.Wind, source => new Wind(
                    source.Wind.Speed, source.Wind.Direction, source.Wind.Gust))
                .Map(dest => dest.Visibility, source => source.Visibility.Value)
                .Map(dest => dest.PrecipitationProbability,
                    source => source.PrecipitationProbability.Value)
                .Map(dest => dest.PrecipitationVolume,
                    source => source.PrecipitationVolume!.Value ?? 0);
        }
    }
}