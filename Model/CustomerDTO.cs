using System.Text.Json.Serialization;

namespace Model
{
    public class CustomerDTO
    {
        [JsonPropertyName("customerName")]
        public string Name { get; set; }

        [JsonPropertyName("customerContact")]
        public string Contact { get; set; }

        [JsonPropertyName("customerCity")]
        public string City { get; set; }

        [JsonPropertyName("customerEmail")]
        public string Email { get; set; }
    }
}
