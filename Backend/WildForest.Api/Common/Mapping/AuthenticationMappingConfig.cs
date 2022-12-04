using Mapster;
using WildForest.Application.Authentication.Common;
using WildForest.Contracts.Authentication;

namespace WildForest.Api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest.Token, src => src.Token)
                .Map(dest => dest.Id, src => src.User.Id.Value);
        }
    }
}