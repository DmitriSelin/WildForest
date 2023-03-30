using ErrorOr;
using WildForest.Application.Marks.Common;

namespace WildForest.Application.Marks.Queries.GetMarks;

public interface IMarkQueryHandler
{
    Task<ErrorOr<List<MarkDto>>> GetMarksByWeatherForecastAsync(Guid weatherId);
}