using WildForest.Application.Common.Models;

namespace WildForest.Application.Comments.Common;

public sealed class CommentDto : Dto
{
    public string Text { get; init; }

    public DateTime Date { get; init; }

    public string FullUserName { get; init; }

    public CommentDto(Guid id, string text, DateTime date, string fullUserName) : base(id)
    {
        Text = text;
        Date = date;
        FullUserName = fullUserName;
    }
}