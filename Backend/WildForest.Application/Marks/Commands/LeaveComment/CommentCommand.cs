namespace WildForest.Application.Marks.Commands.LeaveComment;

public sealed record CommentCommand(Guid WeatherId, Guid UserId, byte Rating, string Comment);