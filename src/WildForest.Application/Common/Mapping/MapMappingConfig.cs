using Mapster;
using WildForest.Application.Maps.Queries.GetCitiesList;
using WildForest.Application.Maps.Queries.GetCountriesList;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Countries.Entities;

namespace WildForest.Application.Common.Mapping;

public sealed class MapMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Country, CountryQuery>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name.Value);

        config.NewConfig<City, CityQuery>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name.Value);
    }
}