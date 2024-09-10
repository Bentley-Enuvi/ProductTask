namespace BAckendCosmosTask.Domain.Entities.ProductAggregates
{
    public class ProductImage
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string ImageUrl { get; set; } = null!;
    }
}
