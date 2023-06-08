using System;
using System.Threading.Tasks;
using WildForest.Frontend.Contracts.Marks;

namespace WildForest.Frontend.Services.Marks.Interfaces;

/// <summary>
/// Serivce for working with comments and marks
/// </summary>
internal interface IMarkService
{
    /// <summary>
    /// Method for getting all comments and avg mark by day weather forecast
    /// </summary>
    /// <param name="weatherId">Id of weather</param>
    /// <param name="token">Jwt token</param>
    /// <returns>MarksResponseBase</returns>
    Task<MarksResponseBase> GetMarksAsync(Guid weatherId, string token);

    /// <summary>
    /// Method for writing new comment
    /// </summary>
    /// <param name="request">HTTP request</param>
    /// <param name="token">Jwt Token</param>
    /// <returns>CommentResponseBase</returns>
    Task<CommentResponseBase> AddCommentWithMarkAsync(CommentRequest request, string token);
}