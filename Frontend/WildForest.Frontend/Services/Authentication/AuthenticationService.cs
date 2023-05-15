using System.Net.Http;
using System.Threading.Tasks;
using WildForest.Frontend.Contracts.Authentication;
using WildForest.Frontend.Services.Authentication.Interfaces;
using System.Text.Json;
using WildForest.Frontend.Common;
using System.Text;

namespace WildForest.Frontend.Services.Authentication
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<ResponseBase> LoginAsync(LoginRequest request)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResponseBase> RegisterAsync(RegisterRequest request)
        {
            string data = JsonSerializer.Serialize(request);
            var payload = new StringContent(data, Encoding.UTF8, ApiItemKeys.AppJson);

            var response = await _httpClient.PostAsync($"{ApiItemKeys.BaseUrl}/auth/register", payload);
            string body = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                body = await response.Content.ReadAsStringAsync();
                var authResponse = JsonSerializer.Deserialize<AuthenticationResponse>(body);

                return new(authResponse, (int)response.StatusCode, null);
            }

            body = await response.Content.ReadAsStringAsync();
            var badResponse = JsonSerializer.Deserialize<BadResponse>(body);

            return new(null, badResponse!.Status, badResponse.Title);
        }
    }
}