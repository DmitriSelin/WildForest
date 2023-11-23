using ErrorOr;
using WildForest.Application.Comments.Common;

namespace WildForest.Application.Comments.Commands.Services;

public interface ICommentService
{
    Task<ErrorOr<CommentDto>> AddCommentAsync(CommentCommand command);

    Task<ErrorOr<CommentDto>> UpdateCommentAsync(CommentCommandForUpdate command);
}