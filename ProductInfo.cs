using Newtonsoft.Json;


namespace FlatRockInterview
{
    public class ProductInfo
    {
        public ProductInfo(string name, string price, decimal rating)
        {
            this.Name = name;
            this.Price = price;
            this.Rating = rating;
        }

        [JsonProperty("productName")]
        public string? Name { get; set; }

        [JsonProperty("price")]
        public string? Price { get; set; }

        [JsonProperty("rating")]
        public decimal Rating { get; set; }
    }

}
