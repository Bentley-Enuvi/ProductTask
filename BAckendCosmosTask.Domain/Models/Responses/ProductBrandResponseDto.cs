using Newtonsoft.Json;


namespace BAckendCosmosTask.Domain.Models.Responses
{
    public class ProductBrandResponseDto
    {
        //[JsonProperty(PropertyName = "id")]
        //public string Id { get; set; }

        public string BrandName { get; set; }

        public string ProductId { get; set; }
    }
}
