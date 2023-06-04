using System;
using System.Text.Json.Serialization;

namespace WildForest.Frontend.Contracts.Marks;

internal class CommentResponse
{
    [JsonPropertyName("markId")]
    public Guid MarkId { get; }

    [JsonPropertyName("userId")]
    public Guid UserId { get; }

    [JsonPropertyName("weatherId")]
    public Guid WeatherId { get; }

    [JsonPropertyName("date")]
    public DateTime Date { get; }

    [JsonPropertyName("rating")]
    public byte Rating { get; }

    [JsonPropertyName("comment")]
    public string Comment { get; }

    [JsonPropertyName("mediumMark")]
    public double MediumMark { get; }

    public CommentResponse(
        Guid markId, Guid userId, Guid weatherId,
        DateTime date, byte rating, string comment,
        double mediumMark)
    {
        MarkId = markId;
        UserId = userId;
        WeatherId = weatherId;
        Date = date;
        Rating = rating;
        Comment = comment;
        MediumMark = mediumMark;
    }
}