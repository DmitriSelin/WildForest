using Mapster;
using WildForest.Application.Maps.Queries.GetCitiesList;
using WildForest.Application.Maps.Queries.GetCountriesList;
using WildForest.Contracts.Maps;

namespace WildForest.Api.Common.Mapping
{
    public class MapsMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CityQuery, CityResponse>();
            config.NewConfig<CountryQuery, CountryResponse>();
        }
    }
}