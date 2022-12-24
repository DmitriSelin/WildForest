using Mapster;
using WildForest.Application.Maps.Queries.GetCitiesList;
using WildForest.Domain.Cities.Entities;

namespace WildForest.Application.Common.Mapping
{
    public class CityMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<City, CityQuery>()
                .Map(dest => dest.CityId, src => src.Id.Value)
                .Map(dest => dest.CityName, src => src.Name);
        }
    }
}