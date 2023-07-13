using Mapster;
using WildForest.Application.Authentication.Commands.Registration.Commands;
using WildForest.Application.Authentication.Common;
using WildForest.Application.Authentication.Queries.LoginUser;
using WildForest.Contracts.Authentication;

namespace WildForest.Api.Common.Mapping;

public sealed class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(LoginRequest, string IpAddress), LoginQuery>()
            .Map(dest => dest.Email, source => source.Item1.Email)
            .Map(dest => dest.Password, source => source.Item1.Password)
            .Map(dest => dest.IpAddress, source => source.IpAddress);

        config.NewConfig<(RegisterRequest, string iPAddress), RegisterCommand>()
            .Map(dest => dest.FirstName, source => source.Item1.FirstName)
            .Map(dest => dest.LastName, source => source.Item1.LastName)
            .Map(dest => dest.Email, source => source.Item1.Email)
            .Map(dest => dest.Password, source => source.Item1.Password)
            .Map(dest => dest.IpAddress, source => source.iPAddress)
            .Map(dest => dest.CityId, source => source.Item1.CityId);

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.AccessToken)
            .Map(dest => dest.Id, src => src.User.Id.Value)
            .Map(dest => dest.FirstName, src => src.User.FirstName.ToString())
            .Map(dest => dest.LastName, src => src.User.LastName.ToString())
            .Map(dest => dest.Email, src => src.User.Email.ToString())
            .Map(dest => dest.Password, src => src.User.Password.ToString())
            .Map(dest => dest.CityId, src => src.User.CityId.Value)
            .Map(dest => dest.CityName, src => src.User.City.Name.Value);
    }
}