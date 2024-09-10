using BAckendCosmosTask.Domain.Models.DTOs;
using BAckendCosmosTask.Domain.Models.Responses;
using BackendProductTask.Domain.Models.Responses;

namespace BackendProductTask.Core.Services.Abstractions
{
    public interface ICategoryService
    {
        //Task<ResponseDto<CategoryResponseDto>> AddCategoryAsync(CategoryDto categoryCreateDto);
        Task<ResponseDto<bool>> DeleteCategoryAsync(string id);
        Task<ResponseDto<IEnumerable<CategoryDto>>> GetAllCategoriesAsync();
        Task<ResponseDto<(IEnumerable<CategoryDto> Categories, string ContinuationToken)>> GetCategoriesAsync(string continuationToken, int pageSize);
        Task<ResponseDto<CategoryDto?>> GetCategoryByIdAsync(string id);
        Task<ResponseDto<CategoryDto>> UpdateCategoryAsync(string id, CategoryDto updateCategoryDto);
    }
}
