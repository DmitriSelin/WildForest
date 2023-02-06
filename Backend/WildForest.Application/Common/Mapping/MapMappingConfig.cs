using Mapster;
using WildForest.Application.Maps.Queries.GetCitiesList;
using WildForest.Application.Maps.Queries.GetCountriesList;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Countries.Entities;

namespace WildForest.Application.Common.Mapping
{
    public sealed class MapMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Country, CountryQuery>()
                .Map(dest => dest.CountryId, src => src.Id.Value)
                .Map(dest => dest.CountryName, src => src.CountryName.Value);

            config.NewConfig<City, CityQuery>()
                .Map(dest => dest.CityId, src => src.Id.Value)
                .Map(dest => dest.CityName, src => src.CityName.Value);
        }
    }
}