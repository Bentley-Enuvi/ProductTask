using BAckendCosmosTask.Domain.Entities.ProductAggregates;
using BAckendCosmosTask.Domain.Entities;
using Newtonsoft.Json;


namespace BackendProductTask.Domain.Models.Responses
{
    public class ProductResponseDto
    {
        public string id { get; set; }
        public string ProductName { get; set; } = null!;

        public string ProductDescription { get; set; } = null!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        //public List<string> ProductBrandIds { get; set; }

        //public List<ProductBrand> ProductBrands { get; set; }

        //public List<string> ImageUrlIds { get; set; }

        //public List<ProductImage> ImageUrls { get; set; }

        public List<string> CategoryIds { get; set; }

        public List<Category> Categories { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }


    //public class ProductResponseDto
    //{
    //    [JsonProperty("id")]
    //    public string Id { get; set; }

    //    public string ProductName { get; set; } = null!;

    //    public string ProductDescription { get; set; } = null!;

    //    public decimal Price { get; set; }

    //    public int Quantity { get; set; }

    //    public Category Category { get; set; }

    //    public ProductBrand ProductBrand { get; set; }

    //    public ProductImage ProductImage { get; set; }

    //    public DateTime CreatedAt { get; set; }

    //    public DateTime UpdatedAt { get; set; }

    //}
}
