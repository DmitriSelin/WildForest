using Mapster;
using WildForest.Contracts.Ratings;
using WildForest.Application.Ratings.Commands.Votes;

namespace WildForest.Api.Common.Mapping;

public sealed class RatingMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<VoteUpdationRequest, VoteUpdationCommand>();
    }
}