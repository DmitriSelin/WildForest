using System.Text.Json.Serialization;

namespace WildForest.Frontend.Contracts.Authentication
{
    public sealed class AuthenticationResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("cityId")]
        public string CityId { get; set; }

        [JsonPropertyName("cityName")]
        public string CityName { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        public AuthenticationResponse(
            string id,
            string firstName,
            string lastName,
            string email,
            string password,
            string cityId,
            string cityName,
            string token)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            CityId = cityId;
            CityName = cityName;
            Token = token;
        }
    }
}