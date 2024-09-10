//using BackendCosmosTask.Infrastructure.CosmosData.Interfaces;
//using BackendCosmosTask.Infrastructure.CosmosData.Repositories;
//using BackendProductTask.Core.Services.Abstractions;
//using BackendProductTask.Core.Services.Implementations;
//using Microsoft.Azure.Cosmos;

//public static class ServiceCollectionExtensions
//{
//    public static IServiceCollection AddCosmosDb(this IServiceCollection services, IConfiguration configuration)
//    {
//        services.AddSingleton(serviceProvider =>
//        {
//            var cosmosClient = new CosmosClient(configuration["CosmosDb:Account"], configuration["CosmosDb:Key"]);
//            var database = cosmosClient.GetDatabase(configuration["CosmosDb:DatabaseName"]);
//            return database;
//        });

//        services.AddScoped(serviceProvider =>
//        {
//            var database = serviceProvider.GetRequiredService<Database>();
//            return database.GetContainer("ProductContainer");
//        });

//        services.AddScoped(serviceProvider =>
//        {
//            var database = serviceProvider.GetRequiredService<Database>();
//            return database.GetContainer("CategoryContainer");
//        });

//        services.AddScoped<IProductCosmosDbRepo>(serviceProvider =>
//        {
//            var productContainer = serviceProvider.GetRequiredService<Container>(); 
//            return new ProductCosmosDbRepo(productContainer); 
//        });

//        services.AddScoped<ICategoryCosmosDb>(serviceProvider =>
//        {
//            var categoryContainer = serviceProvider.GetRequiredService<Container>(); 
//            return new CategoryCosmosDbRepo(categoryContainer); 
//        });

//        services.AddScoped<IProductService, ProductService>();
//        services.AddScoped<ICategoryService, CategoryService>();

//        return services;
//    }
//}


using BackendCosmosTask.Infrastructure.CosmosData.Interfaces;
using BackendCosmosTask.Infrastructure.CosmosData.Repositories;
using BackendProductTask.Core.Services.Abstractions;
using BackendProductTask.Core.Services.Implementations;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCosmosDb(this IServiceCollection services, IConfiguration configuration)
    {
        // Register CosmosClient and Database
        services.AddSingleton(serviceProvider =>
        {
            var cosmosClient = new CosmosClient(configuration["CosmosDb:Account"], configuration["CosmosDb:Key"]);
            return cosmosClient.GetDatabase(configuration["CosmosDb:DatabaseName"]);
        });

        // Register containers for Product and Category
        services.AddScoped(serviceProvider =>
        {
            var database = serviceProvider.GetRequiredService<Database>();
            return database.GetContainer("ProductContainer");
        });

        services.AddScoped(serviceProvider =>
        {
            var database = serviceProvider.GetRequiredService<Database>();
            return database.GetContainer("CategoryContainer");
        });

        // Register repositories with their specific containers
        services.AddScoped<IProductCosmosDbRepo>(serviceProvider =>
        {
            var productContainer = serviceProvider.GetRequiredService<Container>();
            return new ProductCosmosDbRepo(productContainer);
        });

        services.AddScoped<ICategoryCosmosDb>(serviceProvider =>
        {
            var categoryContainer = serviceProvider.GetRequiredService<Container>();
            return new CategoryCosmosDbRepo(categoryContainer);
        });

        // Register services
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();

        return services;
    }
}

