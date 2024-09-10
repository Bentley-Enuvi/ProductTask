using AutoMapper;
using BackendCosmosTask.Infrastructure.CosmosData.Interfaces;
using BAckendCosmosTask.Domain.Entities;
using BAckendCosmosTask.Domain.Models.DTOs;
using BackendProductTask.Core.Services.Abstractions;
using BackendProductTask.Domain.Models.Responses;
using System.Net;

namespace BackendProductTask.Core.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryCosmosDb _cosmosDbRepo;

        public CategoryService(IMapper mapper, ICategoryCosmosDb cosmosDbRepo)
        {
            _mapper = mapper;
            _cosmosDbRepo = cosmosDbRepo;
        }

        public async Task<ResponseDto<CategoryDto?>> GetCategoryByIdAsync(string id)
        {
            try
            {
                var category = await _cosmosDbRepo.GetCategoryByIdAsync(id);
                if (category == null)
                    return ResponseDto<CategoryDto?>.Failure(new List<Error> { new Error("NotFound", "Category not found.") }, (int)HttpStatusCode.NotFound);

                var categoryDto = _mapper.Map<CategoryDto>(category);
                return ResponseDto<CategoryDto?>.Success(categoryDto, "Category retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ResponseDto<CategoryDto?>.Failure(new List<Error> { new Error("GetCategoryByIdError", ex.Message) });
            }
        }

        public async Task<ResponseDto<CategoryDto>> UpdateCategoryAsync(string id, CategoryDto updateCategoryDto)
        {
            try
            {
                var category = await _cosmosDbRepo.GetCategoryByIdAsync(id);
                if (category == null)
                    return ResponseDto<CategoryDto>.Failure(new List<Error> { new Error("NotFound", "Category not found.") }, (int)HttpStatusCode.NotFound);

                _mapper.Map(updateCategoryDto, category);
                await _cosmosDbRepo.UpdateCategoryAsync(id, category);

                var updatedCategoryDto = _mapper.Map<CategoryDto>(category);
                return ResponseDto<CategoryDto>.Success(updatedCategoryDto, "Category updated successfully.");
            }
            catch (Exception ex)
            {
                return ResponseDto<CategoryDto>.Failure(new List<Error> { new Error("UpdateCategoryError", ex.Message) });
            }
        }

        public async Task<ResponseDto<IEnumerable<CategoryDto>>> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await _cosmosDbRepo.GetAllCategoriesAsync(5, null); 
                var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);

                return ResponseDto<IEnumerable<CategoryDto>>.Success(categoryDtos, "Categories retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ResponseDto<IEnumerable<CategoryDto>>.Failure(new List<Error> { new Error("GetAllCategoriesError", ex.Message) });
            }
        }

        public async Task<ResponseDto<(IEnumerable<CategoryDto> Categories, string ContinuationToken)>> GetCategoriesAsync(string continuationToken, int pageSize)
        {
            try
            {
                var categories = await _cosmosDbRepo.GetAllCategoriesAsync(pageSize, continuationToken);

                var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);

                return ResponseDto<(IEnumerable<CategoryDto> Categories, string ContinuationToken)>
                    .Success((categoryDtos, continuationToken), "Categories retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ResponseDto<(IEnumerable<CategoryDto> Categories, string ContinuationToken)>
                    .Failure(new List<Error> { new Error("GetCategoriesError", ex.Message) });
            }
        }

        public async Task<ResponseDto<bool>> DeleteCategoryAsync(string id)
        {
            try
            {
                await _cosmosDbRepo.DeleteCategoryAsync(id);
                return ResponseDto<bool>.Success(true, "Category deleted successfully.");
            }
            catch (Exception ex)
            {
                return ResponseDto<bool>.Failure(new List<Error> { new Error("DeleteCategoryError", ex.Message) });
            }
        }
    }
}
