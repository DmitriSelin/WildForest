using Microsoft.AspNetCore.SignalR;
using WildForest.Api.SignalR.Hubs.Interfaces;
using WildForest.Application.Comments.Commands.Services;
using WildForest.Application.Comments.Common;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Api.SignalR.Hubs;

public sealed class ChatHub : Hub<IChatClient>
{
    private readonly ICommentService _commentService;

    public ChatHub(ICommentService commentService)
    {
        _commentService = commentService;
    }

    public async Task<IEnumerable<CommentDto>> GetComments(Guid weatherId)
    {
        var weatherForecastId = WeatherForecastId.Create(weatherId);

        IEnumerable<CommentDto> comments = await _commentService.GetCommentsAsync(weatherForecastId);

        return comments;
    }
}