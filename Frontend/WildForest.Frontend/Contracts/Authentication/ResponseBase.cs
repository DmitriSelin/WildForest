namespace WildForest.Frontend.Contracts.Authentication
{
    internal class ResponseBase
    {
        public AuthenticationResponse? Response { get; set; }

        public int StatusCode { get; set; }

        public string? Title { get; set; }

        public ResponseBase(AuthenticationResponse? response, int statusCode, string? title)
        {
            Response = response;
            StatusCode = statusCode;
            Title = title;
        }
    }
}