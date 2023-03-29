namespace WildForest.Application.Marks.Commands.PutRating;

public sealed record RatingCommand(Guid WeatherId, Guid UserId, byte Rating);