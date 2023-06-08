using System.Net.Http;
using System.Threading.Tasks;
using WildForest.Frontend.Contracts.Authentication;
using WildForest.Frontend.Services.Authentication.Interfaces;
using System.Text.Json;
using WildForest.Frontend.Common;
using System.Text;
using System.Net;

namespace WildForest.Frontend.Services.Authentication;

/// <summary>
/// Service for register and login
/// </summary>
internal class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;

    public AuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Method for login in app
    /// </summary>
    /// <param name="request">LoginRequest</param>
    /// <returns>ResponseBase</returns>
    public async Task<ResponseBase> LoginAsync(LoginRequest request)
    {
        var payload = CreatePayload(request);

        var response = await _httpClient.PostAsync($"{ApiItemKeys.BaseUrl}/auth/login", payload);

        string body = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return ReturnSuccessResult(body, response.StatusCode);
        }

        return ReturnBadResult(body);
    }

    /// <summary>
    /// Method for registration in app by new user
    /// </summary>
    /// <param name="request">RegisterRequest</param>
    /// <returns>ResponseBase</returns>
    public async Task<ResponseBase> RegisterAsync(RegisterRequest request)
    {
        var payload = CreatePayload(request);

        var response = await _httpClient.PostAsync($"{ApiItemKeys.BaseUrl}/auth/register", payload);
        string body = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return ReturnSuccessResult(body, response.StatusCode);
        }

        return ReturnBadResult(body);
    }

    /// <summary>
    /// Method for creating payload
    /// </summary>
    /// <typeparam name="T">Object for HTTP request</typeparam>
    /// <param name="request">Request</param>
    /// <returns>Payload</returns>
    private StringContent CreatePayload<T>(T request)
    {
        var data = JsonSerializer.Serialize(request);
        var payload = new StringContent(data, Encoding.UTF8, ApiItemKeys.AppJson);

        return payload;
    }

    /// <summary>
    /// Method for returning success result
    /// </summary>
    /// <param name="body">HTTP body</param>
    /// <param name="statusCode">HTTP status code</param>
    /// <returns>ResponseBase</returns>
    private ResponseBase ReturnSuccessResult(string body, HttpStatusCode statusCode)
    {
        var authResponse = JsonSerializer.Deserialize<AuthenticationResponse>(body);

        return new(authResponse, (int)statusCode, null);
    }

    /// <summary>
    /// Method for returning bad result
    /// </summary>
    /// <param name="body">HTTP body</param>
    /// <returns>ResponseBase</returns>
    private ResponseBase ReturnBadResult(string body)
    {
        var badResponse = JsonSerializer.Deserialize<BadResponse>(body);

        return new(null, badResponse!.Status, badResponse.Title);
    }
}