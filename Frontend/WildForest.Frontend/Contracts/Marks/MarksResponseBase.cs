using System.Collections.Generic;

namespace WildForest.Frontend.Contracts.Marks;

internal class MarksResponseBase
{
    public List<CommentsModel>? Comments { get; set; }

    public int StatusCode { get; set; }

    public string? Title { get; set; }

    public MarksResponseBase(List<CommentsModel>? comments, int statusCode, string? title)
    {
        Comments = comments;
        StatusCode = statusCode;
        Title = title;
    }
}