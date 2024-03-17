using ErrorOr;
using WildForest.Application.Comments.Common;
using WildForest.Contracts.Comments;

namespace WildForest.Api.SignalR.Hubs.Interfaces;

public interface IChatClient
{
    Task SendCommentAsync(CommentDto comment);

    Task UpdateCommentAsync(CommentRequestForUpdate request);

    Task Error(Error error);
}