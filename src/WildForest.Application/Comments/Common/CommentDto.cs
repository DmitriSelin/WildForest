using WildForest.Dto.Models;

namespace WildForest.Application.Comments.Common;

public sealed class CommentDto : BaseDto
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