namespace BAckendCosmosTask.Domain.Entities.ProductAggregates
{
    public class ProductBrand
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = null!;
    }
}
