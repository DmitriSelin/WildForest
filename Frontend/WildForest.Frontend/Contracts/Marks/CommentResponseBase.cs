namespace WildForest.Frontend.Contracts.Marks;

internal class CommentResponseBase
{
    public CommentResponse? Comment { get; set; }

    public int StatusCode { get; set; }

    public string? Title { get; set; }

    public CommentResponseBase(CommentResponse comment, int statusCode, string? title)
    {
        Comment = comment;
        StatusCode = statusCode;
        Title = title;
    }
}