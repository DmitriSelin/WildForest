using WildForest.Application.Marks.Common.Base;

namespace WildForest.Application.Marks.Common;

public sealed class CommentDto : MarkModel
{
    public string Comment { get; }

    public double MediumMark { get; }

    public CommentDto(
        Guid markId,
        Guid userId,
        Guid weatherId,
        DateTime date,
        byte rating,
        string comment,
        double mediumMark) : base(markId, userId, weatherId, date, rating)
        {
            Comment = comment;
            MediumMark = mediumMark;
        }
}