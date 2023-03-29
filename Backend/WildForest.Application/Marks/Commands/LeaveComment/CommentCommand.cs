namespace WildForest.Application.Marks.Commands.LeaveComment;

public sealed record CommentCommand(Guid WeatherId, Guid UserId, byte StarsCount, string Comment);