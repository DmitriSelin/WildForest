using ErrorOr;
using WildForest.Application.Marks.Common;

namespace WildForest.Application.Marks.Commands.LeaveComment;

public interface ICommentCommandHandler
{
    Task<ErrorOr<CommentDto>> LeaveCommentAsync(CommentCommand command);
}