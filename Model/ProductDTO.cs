using System.Text.Json.Serialization;

namespace Model
{
    public class ProductDTO
    {
        [JsonIgnore]
        public string? ProductGUID { get; set; }

        [JsonPropertyName("productName")]
        public string? ProductName { get; set; }

        [JsonPropertyName("manufacture")]
        public string? Manufacture { get; set; }

        [JsonPropertyName("stock")]
        public int Stock { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }

    public class ProductDeleteDTO
    {
        [JsonPropertyName("productGUID")]
        public string? ProductGUID { get; set; }
    }
}
