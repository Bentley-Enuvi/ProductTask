using BAckendCosmosTask.Domain.Models.DTOs;
using BackendProductTask.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BackendProductTaskAPI.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

       

        [HttpDelete("delete-single-category")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var response = await _categoryService.DeleteCategoryAsync(id);
            if (response.IsSuccessful)
                return Ok(response);

            return BadRequest(response);
        }


        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await _categoryService.GetAllCategoriesAsync();
            if (response.IsSuccessful)
                return Ok(response);

            return BadRequest(response);
        }


        [HttpGet("get-all-load-page")]
        public async Task<IActionResult> GetCategories([FromQuery] string continuationToken, [FromQuery] int pageSize)
        {
            var response = await _categoryService.GetCategoriesAsync(continuationToken, pageSize);
            if (response.IsSuccessful)
                return Ok(response);

            return BadRequest(response);
        }


        [HttpGet("get-single-category")]
        public async Task<IActionResult> GetCategoryById(string id)
        {
            var response = await _categoryService.GetCategoryByIdAsync(id);
            if (response.IsSuccessful)
                return Ok(response);

            return NotFound(response);
        }


        [HttpPut("update-category")]
        public async Task<IActionResult> UpdateCategory(string id, [FromBody] CategoryDto updateCategoryDto)
        {
            var response = await _categoryService.UpdateCategoryAsync(id, updateCategoryDto);
            if (response.IsSuccessful)
                return Ok(response);

            return BadRequest(response);
        }


        

    }
}
