using WildForest.Contracts.Comments;

namespace WildForest.Api.SignalR.Hubs.Interfaces;

public interface IChatClient
{
    Task SendCommentAsync(CommentRequest request);

    Task UpdateCommentAsync(CommentRequestForUpdate request);
}