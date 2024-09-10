using AutoMapper;
using BackendProductTask.Domain.Models.Responses;
using BackendProductTask.Core.Services.Abstractions;
using System.Net;
using BAckendCosmosTask.Domain.Models.DTOs;
using BackendCosmosTask.Infrastructure.CosmosData.Interfaces;
using BAckendCosmosTask.Domain.Entities;
using Microsoft.Azure.Cosmos;

namespace BackendProductTask.Core.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductCosmosDbRepo _productCosmosDbRepo;
        private readonly ICategoryCosmosDb _categoryCosmosDb;

        public ProductService(IMapper mapper, IProductCosmosDbRepo productCosmosDbRepo, ICategoryCosmosDb categoryCosmosDb)
        {
            _mapper = mapper;
            _productCosmosDbRepo = productCosmosDbRepo;
            _categoryCosmosDb = categoryCosmosDb;
        }

        public async Task<ResponseDto<ProductResponseDto>> AddProductAsync(ProductDto createDto)
        {
            try
            {
                var product = _mapper.Map<Product>(createDto);
                product.id = Guid.NewGuid().ToString();
                product.CreatedAt = DateTime.UtcNow;
                product.UpdatedAt = DateTime.UtcNow;

                foreach (var category in product.Categories)
                {
                    // Generate a new ID for each category and assign the ProductId
                    category.id = Guid.NewGuid().ToString();
                    category.ProductId = product.id;

                    // Add the category to the Category container in Cosmos DB
                    await _categoryCosmosDb.AddCategoryAsync(category);
                }

                await _productCosmosDbRepo.AddProductAsync(product);

                var responseDto = _mapper.Map<ProductResponseDto>(product);
                return ResponseDto<ProductResponseDto>.Success(responseDto, "Product created successfully");
            }
            catch (Exception ex)
            {
                var errors = new List<Error> { new Error("AddProductError", ex.Message) };
                return ResponseDto<ProductResponseDto>.Failure(errors, (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ResponseDto<bool>> DeleteProductAsync(string id)
        {
            try
            {
                await _productCosmosDbRepo.DeleteProductAsync(id);
                return ResponseDto<bool>.Success(true, $"Product with ID '{id}' deleted successfully.");
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                var errors = new List<Error> { new Error("NotFound", $"Product with ID '{id}' not found.") };
                return ResponseDto<bool>.Failure(errors, (int)HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                var errors = new List<Error> { new Error("DeleteProductError", ex.Message) };
                return ResponseDto<bool>.Failure(errors, (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ResponseDto<List<ProductResponseDto>>> GetAllProductsAsync()
        {
            try
            {
                var products = await _productCosmosDbRepo.GetAllProductsAsync();
                var productDtos = _mapper.Map<List<ProductResponseDto>>(products);

                return ResponseDto<List<ProductResponseDto>>.Success(productDtos, "Products retrieved successfully");
            }
            catch (Exception ex)
            {
                var errors = new List<Error> { new Error("GetAllProductsError", ex.Message) };
                return ResponseDto<List<ProductResponseDto>>.Failure(errors, (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ResponseDto<ProductResponseDto>> GetProductByIdAsync(string id)
        {
            try
            {
                var product = await _productCosmosDbRepo.GetProductByIdAsync(id);

                if (product == null)
                {
                    var errors = new List<Error> { new Error("NotFound", $"Product with ID '{id}' not found.") };
                    return ResponseDto<ProductResponseDto>.Failure(errors, (int)HttpStatusCode.NotFound);
                }

                var productDto = _mapper.Map<ProductResponseDto>(product);

                return ResponseDto<ProductResponseDto>.Success(productDto, "Product retrieved successfully");
            }
            catch (Exception ex)
            {
                var errors = new List<Error> { new Error("GetProductByIdError", ex.Message) };
                return ResponseDto<ProductResponseDto>.Failure(errors, (int)HttpStatusCode.InternalServerError);
            }
        }

        //public async Task<ResponseDto<(IEnumerable<ProductResponseDto> Products, string ContinuationToken)>> GetProductsAsync(string continuationToken, int pageSize)
        //{
        //    try
        //    {
        //        var result = await _productCosmosDbRepo.GetProductsAsync(continuationToken, pageSize);
        //        var productDtos = _mapper.Map<IEnumerable<ProductResponseDto>>(result.Products);

        //        return ResponseDto<(IEnumerable<ProductResponseDto> Products, string ContinuationToken)>.Success(
        //            (productDtos, result.ContinuationToken)
        //        );
        //    }
        //    catch (Exception ex)
        //    {
        //        var errors = new List<Error> { new Error("GetProductsError", ex.Message) };
        //        return ResponseDto<(IEnumerable<ProductResponseDto> Products, string ContinuationToken)>.Failure(errors, (int)HttpStatusCode.InternalServerError);
        //    }
        //}

        public async Task<ResponseDto<ProductResponseDto>> UpdateProductAsync(string id, UpdateProductDto updateDto)
        {
            try
            {
                var product = await _productCosmosDbRepo.GetProductByIdAsync(id);

                if (product == null)
                    return ResponseDto<ProductResponseDto>.Failure(new[] { new Error("NotFound", "Product not found.") }, 404);

                product.Price = updateDto.Price;
                product.UpdatedAt = DateTime.UtcNow;

                await _productCosmosDbRepo.UpdateProductAsync(id, product);

                var productDto = _mapper.Map<ProductResponseDto>(product);
                return ResponseDto<ProductResponseDto>.Success(productDto, "Product updated successfully");
            }
            catch (Exception ex)
            {
                var errors = new List<Error> { new Error("UpdateProductError", ex.Message) };
                return ResponseDto<ProductResponseDto>.Failure(errors, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
