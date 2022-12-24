using Mapster;
using WildForest.Application.Maps.Common;
using WildForest.Domain.Countries.Entities;

namespace WildForest.Application.Common.Mapping
{
    public class CountryMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Country, CountryViewModel>()
                .Map(dest => dest.CountryId, src => src.Id.Value)
                .Map(dest => dest.CountryName, src => src.Name);
        }
    }
}