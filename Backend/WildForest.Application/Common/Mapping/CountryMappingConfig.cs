using Mapster;
using WildForest.Domain.Countries.Entities;
using WildForest.Application.Maps.Queries.GetCountriesList;

namespace WildForest.Application.Common.Mapping
{
    public class CountryMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Country, CountryQuery>()
                .Map(dest => dest.CountryId, src => src.Id.Value)
                .Map(dest => dest.CountryName, src => src.Name);
        }
    }
}