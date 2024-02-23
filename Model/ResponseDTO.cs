using System.Text.Json.Serialization;

namespace Model
{
    public class ResponseDTO
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("error_code")]
        public int? Error_Code { get; set; }

        [JsonPropertyName("data")]
        public object? Data { get; set; }
    }
}
