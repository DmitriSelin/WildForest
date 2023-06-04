using WildForest.Application.Marks.Common.Base;

namespace WildForest.Application.Marks.Common;

public sealed class CommentsModel : MarkModel
{
    public string FirstName { get; }

    public string LastName { get; }

    public string Comment { get; }

    public CommentsModel(
        Guid markId,
        Guid userId,
        Guid weatherId,
        DateTime date,
        byte rating,
        string firstName,
        string lastName,
        string comment) : base(markId, userId, weatherId, date, rating)
    {
        FirstName = firstName;
        LastName = lastName;
        Comment = comment;
    }
}