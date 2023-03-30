namespace WildForest.Application.Marks.Common;

public sealed record MarkDto(
    Guid MarkId,
    Guid UserId,
    Guid WeatherId,
    DateTime Date,
    byte Rating,
    string? Comment);