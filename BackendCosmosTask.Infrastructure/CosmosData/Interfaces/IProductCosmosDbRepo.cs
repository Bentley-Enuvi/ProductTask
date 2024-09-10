using BAckendCosmosTask.Domain.Entities;
using Microsoft.Azure.Cosmos;

namespace BackendCosmosTask.Infrastructure.CosmosData.Interfaces
{
    public interface IProductCosmosDbRepo
    {
        //Task<IEnumerable<Product>> GetAllProductsAsync(int pageSize, string continuationToken);
        //Task<Product> GetProductByIdAsync(string id);
        //Task AddProductsAsync(IEnumerable<Product> products);
        //Task UpdateProductAsync(string id, Product product);
        //Task DeleteProductAsync(string id);

        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(string id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(string id, Product product);
        Task DeleteProductAsync(string id);
        //Task<(IEnumerable<Product> Products, string ContinuationToken)> GetProductsAsync(string continuationToken, int pageSize);

    }
}