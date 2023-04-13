using WildForest.Application.Marks.Common;
using ErrorOr;

namespace WildForest.Application.Marks.Queries.GetComments;

public interface ICommentsQueryHandler
{
    Task<ErrorOr<List<CommentsModel>>> GetCommentsAsync(Guid weatherId);
}