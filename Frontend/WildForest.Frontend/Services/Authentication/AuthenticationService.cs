using System.Net.Http;
using System.Threading.Tasks;
using WildForest.Frontend.Contracts.Authentication;
using WildForest.Frontend.Services.Authentication.Interfaces;
using System.Text.Json;
using WildForest.Frontend.Common;
using System.Text;
using System.Net;

namespace WildForest.Frontend.Services.Authentication
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

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

        private StringContent CreatePayload<T>(T request)
        {
            var data = JsonSerializer.Serialize(request);
            var payload = new StringContent(data, Encoding.UTF8, ApiItemKeys.AppJson);

            return payload;
        }

        private ResponseBase ReturnSuccessResult(string body, HttpStatusCode statusCode)
        {
            var authResponse = JsonSerializer.Deserialize<AuthenticationResponse>(body);

            return new(authResponse, (int)statusCode, null);
        }

        private ResponseBase ReturnBadResult(string body)
        {
            var badResponse = JsonSerializer.Deserialize<BadResponse>(body);

            return new(null, badResponse!.Status, badResponse.Title);
        }
    }
}