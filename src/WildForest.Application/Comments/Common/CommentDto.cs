namespace WildForest.Application.Comments.Common;

public sealed record CommentDto(
    Guid Id,
    string Text,
    DateTime Date,
    string FullUserName);