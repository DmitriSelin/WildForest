using WildForest.Application.Marks.Common;
using ErrorOr;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Application.Marks.Queries.GetComments;

public interface ICommentsQueryHandler
{
    Task<ErrorOr<List<MarkDto>>> GetCommentsAsync(Guid weatherId);
}