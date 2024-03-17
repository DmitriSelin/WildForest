using WildForest.Dto.Models;

namespace WildForest.Application.Comments.Common;

public sealed class CommentDto : BaseDto
{
    public string Text { get; init; }

    public DateTime Date { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public byte[]? Image { get; init; }

    public CommentDto(Guid id, string text, DateTime date,
        string firstName, string lastName, byte[]? image) : base(id)
    {
        Text = text;
        Date = date;
        FirstName = firstName;
        LastName = lastName;
        Image = image;
    }
}