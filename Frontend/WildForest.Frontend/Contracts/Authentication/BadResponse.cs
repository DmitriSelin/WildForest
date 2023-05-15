using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WildForest.Frontend.Contracts.Authentication
{
    public class BadResponse
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("traceId")]
        public string TraceId { get; set; }

        [JsonPropertyName("errorCodes")]
        public List<string> ErrorCodes { get; set; }

        public BadResponse(
            string type,
            string title,
            int status,
            string traceId,
            List<string> errorCodes)
        {
            Type = type;
            Title = title;
            Status = status;
            TraceId = traceId;
            ErrorCodes = errorCodes;
        }
    }
}