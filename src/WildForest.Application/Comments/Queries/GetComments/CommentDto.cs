namespace WildForest.Application.Comments.Queries.GetComments;

public sealed record CommentDto(
    Guid Id,
    string Text,
    DateTime Date,
    string FullUserName);