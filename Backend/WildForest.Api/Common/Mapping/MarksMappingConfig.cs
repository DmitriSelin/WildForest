using Mapster;
using WildForest.Application.Marks.Commands.LeaveComment;
using WildForest.Contracts.Marks;

namespace WildForest.Api.Common.Mapping;

public sealed class MarksMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CommentRequest, CommentCommand>();
    }
}