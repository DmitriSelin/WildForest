using Mapster;
using WildForest.Application.Maps.Queries.GetCountriesList;
using WildForest.Contracts.Maps;

namespace WildForest.Api.Common.Mapping
{
    public class CountryMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CountryQuery, CountriesResponse>();
        }
    }
}