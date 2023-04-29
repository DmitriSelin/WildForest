namespace WildForest.Application.Marks.Common.Base;

public abstract class MarkModel
{
    public Guid MarkId { get; }

    public Guid UserId { get; }

    public Guid WeatherId { get; }

    public DateTime Date { get; }

    public byte Rating { get; }

    protected MarkModel(
        Guid markId,
        Guid userId,
        Guid weatherId,
        DateTime date,
        byte rating)
    {
        MarkId = markId;
        UserId = userId;
        WeatherId = weatherId;
        Date = date;
        Rating = rating;
    }
}