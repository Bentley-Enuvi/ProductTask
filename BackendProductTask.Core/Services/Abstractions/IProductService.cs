using BAckendCosmosTask.Domain.Models.DTOs;
using BackendProductTask.Domain.Models.Responses;

namespace BackendProductTask.Core.Services.Abstractions
{
    public interface IProductService
    {
        //Task<ResponseDto<ProductResponseDto>> AddProductAsync(ProductDto createDto);
        //Task<ResponseDto<bool>> DeleteProductAsync(string id);
        //Task<ResponseDto<List<ProductResponseDto>>> GetAllProductsAsync();
        //Task<ResponseDto<ProductResponseDto>> GetProductByIdAsync(string id);
        //Task<ResponseDto<(IEnumerable<ProductResponseDto> Products, string ContinuationToken)>> GetProductsAsync(string continuationToken, int pageSize);
        //Task<ResponseDto<ProductResponseDto>> UpdateProductAsync(string id, UpdateProductDto updateDto);
        //Task<(List<ProductDto> Products, string ContinuationToken)> GetAllProductsAsync(string continuationToken = null);

        Task<ResponseDto<ProductResponseDto>> AddProductAsync(ProductDto createDto);
        Task<ResponseDto<bool>> DeleteProductAsync(string id);
        Task<ResponseDto<List<ProductResponseDto>>> GetAllProductsAsync();
        Task<ResponseDto<ProductResponseDto>> GetProductByIdAsync(string id);
        //Task<ResponseDto<(IEnumerable<ProductResponseDto> Products, string ContinuationToken)>> GetProductsAsync(string continuationToken, int pageSize);
        Task<ResponseDto<ProductResponseDto>> UpdateProductAsync(string id, UpdateProductDto updateDto);

    }
}
