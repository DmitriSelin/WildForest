using Mapster;
using WildForest.Application.Marks.Common;
using WildForest.Domain.Marks.Entities;

namespace WildForest.Application.Common.Mapping;

public sealed class MarkMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Mark, CommentsModel>()
            .Map(dest => dest.MarkId, source => source.Id.Value)
            .Map(dest => dest.UserId, source => source.UserId.Value)
            .Map(dest => dest.WeatherId, source => source.WeatherId.Value)
            .Map(dest => dest.Date, source => source.Date.Value)
            .Map(dest => dest.Rating, source => source.Rating.Value)
            .Map(dest => dest.FirstName, source => source.User.FirstName.Value)
            .Map(dest => dest.LastName, source => source.User.LastName.Value)
            .Map(dest => dest.Comment, source => source.Comment!.Value);
    }
}