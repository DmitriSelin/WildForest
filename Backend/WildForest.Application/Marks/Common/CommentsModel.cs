using WildForest.Application.Marks.Common.Base;

namespace WildForest.Application.Marks.Common;

public sealed class CommentsModel : MarkModel
{
    public string Comment { get; }

    public CommentsModel(
        Guid markId,
        Guid userId,
        Guid weatherId,
        DateTime date,
        byte rating,
        string comment) : base(markId, userId, weatherId, date, rating)
    {
        Comment = comment;
    }
}