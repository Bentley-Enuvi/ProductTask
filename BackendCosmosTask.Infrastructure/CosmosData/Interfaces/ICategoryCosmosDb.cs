using BAckendCosmosTask.Domain.Entities;
using Microsoft.Azure.Cosmos;

namespace BackendCosmosTask.Infrastructure.CosmosData.Interfaces
{
    public interface ICategoryCosmosDb
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync(int pageSize, string continuationToken);
        Task<Category> GetCategoryByIdAsync(string id);
        Task AddCategoryAsync(IEnumerable<Category> categories); 
        Task UpdateCategoryAsync(string id, Category category);
        Task DeleteCategoryAsync(string id);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(string categoryId);
    }
}