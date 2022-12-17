using Mapster;
using WildForest.Application.Authentication.Common;
using WildForest.Contracts.Authentication;

namespace WildForest.Api.Common.Mapping
{
    public sealed class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest.Token, src => src.Token)
                .Map(dest => dest.Id, src => src.User.Id.Value)
                .Map(dest => dest.FirstName, src => src.User.FirstName)
                .Map(dest => dest.LastName, src => src.User.LastName)
                .Map(dest => dest.Email, src => src.User.Email)
                .Map(dest => dest.Password, src => src.User.Password);
        }
    }
}