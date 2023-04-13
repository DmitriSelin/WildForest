using WildForest.Application.Marks.Common.Base;

namespace WildForest.Application.Marks.Common;

public sealed class RatingDto : MarkModel
{
    public double MediumMark { get; }

    public RatingDto(
        Guid markId,
        Guid userId,
        Guid weatherId,
        DateTime date,
        byte rating,
        double mediumMark) : base(markId, userId, weatherId, date, rating)
    {
        MediumMark = mediumMark;
    }
}