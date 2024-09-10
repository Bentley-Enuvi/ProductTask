using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendCosmosTask.Infrastructure.CosmosData.Interfaces
{
    public interface ICosmosDb
    {
        Task AddItemAsync<T>(T item, string containerName);
        Task<T> GetItemAsync<T>(string id, string containerName);
        Task UpdateItemAsync<T>(string id, T item, string containerName);
        Task DeleteItemAsync<T>(string id, string containerName);
        Task<IEnumerable<T>> GetItemsAsync<T>(string queryString, string containerName);

    }
}
