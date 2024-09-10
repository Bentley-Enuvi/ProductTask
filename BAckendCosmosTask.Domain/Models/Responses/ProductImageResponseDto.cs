using Newtonsoft.Json;


namespace BAckendCosmosTask.Domain.Models.Responses
{
    public class ProductImageResponseDto
    {
        //[JsonProperty(PropertyName = "id")]
        //public string Id { get; set; }

        public string ImageUrl { get; set; } 

        public string ProductId { get; set; }
    }
}
