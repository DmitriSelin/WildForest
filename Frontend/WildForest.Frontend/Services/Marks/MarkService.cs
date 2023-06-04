using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WildForest.Frontend.Common;
using WildForest.Frontend.Contracts.Authentication;
using WildForest.Frontend.Contracts.Marks;
using WildForest.Frontend.Services.Marks.Interfaces;

namespace WildForest.Frontend.Services.Marks;

internal class MarkService : IMarkService
{
    private readonly HttpClient _httpClient;

    public MarkService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CommentResponseBase> AddCommentWithMarkAsync(CommentRequest request, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var payload = CreatePayload(request);

        var response = await _httpClient.PostAsync($"{ApiItemKeys.BaseUrl}/weather/marks/comment", payload);

        string body = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var comment = JsonSerializer.Deserialize<CommentResponse>(body);
            return new(comment!, (int)response.StatusCode, null);
        }

        var badResponse = JsonSerializer.Deserialize<BadResponse>(body);
        return new(null!, badResponse!.Status, badResponse.Title);
    }

    public async Task<MarksResponseBase> GetMarksAsync(Guid weatherId, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.GetAsync($"{ApiItemKeys.BaseUrl}/weather/marks/{weatherId}");

        string body = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var comments = JsonSerializer.Deserialize<List<CommentsModel>>(body);
            return new(comments, (int)response.StatusCode, null);
        }

        var badResponse = JsonSerializer.Deserialize<BadResponse>(body);
        return new(null, badResponse!.Status, badResponse.Title);
    }

    private StringContent CreatePayload<T>(T request)
    {
        var data = JsonSerializer.Serialize(request);
        var payload = new StringContent(data, Encoding.UTF8, ApiItemKeys.AppJson);

        return payload;
    }
}