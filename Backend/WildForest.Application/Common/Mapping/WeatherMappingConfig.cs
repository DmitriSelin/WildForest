using Mapster;
using WildForest.Application.Weather.Common;
using WildForest.Domain.Weather.Entities;

namespace WildForest.Application.Common.Mapping
{
    public sealed class WeatherMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<WeatherForecast, WeatherForecustDto>();
        }
    }
}