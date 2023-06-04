using System;
using System.Threading.Tasks;
using WildForest.Frontend.Contracts.Marks;

namespace WildForest.Frontend.Services.Marks.Interfaces;

internal interface IMarkService
{
    Task<MarksResponseBase> GetMarksAsync(Guid weatherId, string token);

    Task<CommentResponseBase> AddCommentWithMarkAsync(CommentRequest request, string token);
}