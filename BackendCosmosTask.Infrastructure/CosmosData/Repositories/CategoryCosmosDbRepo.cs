using BackendCosmosTask.Infrastructure.CosmosData.Interfaces;
using BAckendCosmosTask.Domain.Entities;
using Microsoft.Azure.Cosmos;
using System.Net;


namespace BackendCosmosTask.Infrastructure.CosmosData.Repositories
{
    public class CategoryCosmosDbRepo : ICategoryCosmosDb
    {
        private readonly ICosmosDb _cosmosDbService;
        private readonly string _containerName = "CategoryClass";

        public CategoryCosmosDbRepo(ICosmosDb cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(int pageSize, string continuationToken)
        {
            var queryString = $"SELECT * FROM c OFFSET {continuationToken} LIMIT {pageSize}";
            return await _cosmosDbService.GetItemsAsync<Category>(queryString, _containerName);
        }

        public async Task<Category> GetCategoryByIdAsync(string id)
        {
            return await _cosmosDbService.GetItemAsync<Category>(id, _containerName);
        }

        public async Task AddCategoryAsync(IEnumerable<Category> categories)
        {
            foreach (var category in categories)
            {
                category.id = Guid.NewGuid().ToString();
                await _cosmosDbService.AddItemAsync(category, _containerName);
            }
        }

        public async Task UpdateCategoryAsync(string id, Category category)
        {
            category.id = id;
            await _cosmosDbService.UpdateItemAsync(id, category, _containerName);
        }

        public async Task DeleteCategoryAsync(string id)
        {
            await _cosmosDbService.DeleteItemAsync<Category>(id, _containerName);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string categoryId)
        {
            var queryString = $"SELECT * FROM c WHERE ARRAY_CONTAINS(c.CategoryIds, '{categoryId}')";
            return await _cosmosDbService.GetItemsAsync<Product>(queryString, "Product");
        }
    }
}
