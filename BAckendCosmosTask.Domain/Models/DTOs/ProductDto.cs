namespace BAckendCosmosTask.Domain.Models.DTOs
{
    public class ProductDto
    {
        public string ProductName { get; set; } = null!;

        public string ProductDescription { get; set; } = null!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public IList<CategoryDto> Categories { get; set; }
    }


}
