namespace WildForest.Application.Marks.Common;

public sealed record MarkDto(Guid MarkId, Guid VoteId, byte VoteResult, int Points);