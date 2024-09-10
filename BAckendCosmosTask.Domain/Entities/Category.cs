using Newtonsoft.Json;

namespace BAckendCosmosTask.Domain.Entities
{
    public class Category
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; } = Guid.NewGuid().ToString();

        public string CategoryName { get; set; }

        public string ProductId { get; set; }

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;

    }
}
