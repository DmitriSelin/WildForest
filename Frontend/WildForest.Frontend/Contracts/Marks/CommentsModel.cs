using System;
using System.Text.Json.Serialization;

namespace WildForest.Frontend.Contracts.Marks;

internal sealed class CommentsModel
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

    [JsonPropertyName("firstName")]
    public string FirstName { get; }

    [JsonPropertyName("lastName")]
    public string LastName { get; }

    [JsonPropertyName("comment")]
    public string Comment { get; }

    public CommentsModel(
        Guid markId, Guid userId, Guid weatherId,
        DateTime date, byte rating, string firstName,
        string lastName, string comment)
    {
        MarkId = markId;
        UserId = userId;
        WeatherId = weatherId;
        Date = date;
        Rating = rating;
        FirstName = firstName;
        LastName = lastName;
        Comment = comment;
    }
}