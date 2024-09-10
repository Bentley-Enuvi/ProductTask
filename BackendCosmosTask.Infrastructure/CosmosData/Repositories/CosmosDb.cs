using BackendCosmosTask.Infrastructure.CosmosData.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendCosmosTask.Infrastructure.CosmosData.Repositories
{
    public class CosmosDb : ICosmosDb
    {
        private readonly CosmosClient _cosmosClient;
        private readonly string _databaseName;

        public CosmosDb(CosmosClient cosmosClient, IConfiguration configuration)
        {
            _cosmosClient = cosmosClient;
            _databaseName = configuration["CosmosDb:DatabaseName"];
        }

        private Container GetContainer(string containerName)
        {
            return _cosmosClient.GetContainer(_databaseName, containerName);
        }

        public async Task AddItemAsync<T>(T item, string containerName)
        {
            var container = GetContainer(containerName);
            await container.CreateItemAsync(item, new PartitionKey((item as dynamic).id));
        }

        public async Task<T> GetItemAsync<T>(string id, string containerName)
        {
            var container = GetContainer(containerName);
            return await container.ReadItemAsync<T>(id, new PartitionKey(id));
        }

        public async Task UpdateItemAsync<T>(string id, T item, string containerName)
        {
            var container = GetContainer(containerName);
            await container.UpsertItemAsync(item, new PartitionKey(id));
        }

        public async Task DeleteItemAsync<T>(string id, string containerName)
        {
            var container = GetContainer(containerName);
            await container.DeleteItemAsync<T>(id, new PartitionKey(id));
        }

        public async Task<IEnumerable<T>> GetItemsAsync<T>(string queryString, string containerName)
        {
            var container = GetContainer(containerName);
            var query = container.GetItemQueryIterator<T>(new QueryDefinition(queryString));
            var results = new List<T>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }


    }



}
