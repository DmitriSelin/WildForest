namespace WildForest.Application.Marks.Common;

public sealed record MarkDto(
    Guid MarkId,
    Guid WeatherId,
    DateTime Date,
    byte StarsCount,
    string? Comment);