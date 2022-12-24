using Mapster;
using WildForest.Application.Maps.Common;
using WildForest.Contracts.Maps;

namespace WildForest.Api.Common.Mapping
{
    public class CountryMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CountryViewModel, CountriesResponse>();
        }
    }
}