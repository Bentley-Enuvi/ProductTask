using BackendCosmosTask.Infrastructure.CosmosData.Interfaces;
using Microsoft.Azure.Cosmos;
using BAckendCosmosTask.Domain.Entities;

namespace BackendCosmosTask.Infrastructure.CosmosData.Repositories
{
    public class ProductCosmosDbRepo : IProductCosmosDbRepo
    {
        private readonly ICosmosDb _cosmosDbService;
        private readonly string _containerName = "ProductClass";

        public ProductCosmosDbRepo(ICosmosDb cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var queryString = "SELECT * FROM c";
            return await _cosmosDbService.GetItemsAsync<Product>(queryString, _containerName);
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _cosmosDbService.GetItemAsync<Product>(id, _containerName);
        }

        public async Task AddProductAsync(Product product)
        {
            await _cosmosDbService.AddItemAsync(product, _containerName);
        }

        public async Task UpdateProductAsync(string id, Product product)
        {
            await _cosmosDbService.UpdateItemAsync(id, product, _containerName);
        }

        public async Task DeleteProductAsync(string id)
        {
            await _cosmosDbService.DeleteItemAsync<Product>(id, _containerName);
        }

        //public async Task<(IEnumerable<Product> Products, string ContinuationToken)> GetProductsAsync(string continuationToken, int pageSize)
        //{
        //    var queryString = "SELECT * FROM c";
        //    var items = await _cosmosDbService.GetItemsAsync<Product>(queryString, _containerName, continuationToken, pageSize);
        //    return items;
        //}
    }
}
