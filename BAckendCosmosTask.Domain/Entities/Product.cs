using Newtonsoft.Json;


namespace BAckendCosmosTask.Domain.Entities
{
    public class Product
    {
        [JsonProperty("id")]
        public string id { get; set; } = Guid.NewGuid().ToString();

        public string ProductName { get; set; } = null!;

        public string ProductDescription { get; set; } = null!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public List<Category> Categories { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
