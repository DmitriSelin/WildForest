using Mapster;
using WildForest.Application.Maps.Queries.GetCitiesList;
using WildForest.Contracts.Maps;

namespace WildForest.Api.Common.Mapping
{
    public class CityMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CityQuery, CityResponse>();
        }
    }
}