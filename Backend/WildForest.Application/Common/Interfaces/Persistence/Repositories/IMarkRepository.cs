using WildForest.Domain.Marks.Entities;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories;

public interface IMarkRepository
{
    Task AddMarkAsync(Mark mark);

    Task<IEnumerable<Mark>?> GetMarksWithCommentsByWeatherIdAsync(WeatherId weatherId);

    Task<Mark?> GetMarkByWeatherIdAndUserIdAsync(WeatherId weatherId, UserId userId);
}