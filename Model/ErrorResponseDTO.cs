using System.Text.Json.Serialization;

namespace Model
{
    public class ErrorResponseDTO
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("status")]
        public int? Status { get; set; }

        [JsonPropertyName("detail")]
        public string? Detail { get; set; }

        [JsonPropertyName("instances")]
        public string? Instances { get; set; }
    }
}
