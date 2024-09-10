using BAckendCosmosTask.Domain.Models.DTOs;
using BackendProductTask.Core.Services.Abstractions;
using BackendProductTask.Domain.Models.Responses;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }


    [HttpPost("add-product")]
    public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                           .Select(e => new Error("ModelValidationError", e.ErrorMessage));
            return BadRequest(ResponseDto<ProductResponseDto>.Failure(errors));
        }


        var result = await _productService.AddProductAsync(productDto);
        if (result.IsSuccessful)
        {
            return Ok(result);
        }

        return StatusCode(result.Code, result);
    }


    [HttpGet("product/{id}")]
    public async Task<IActionResult> GetProductById(string id)
    {
        var result = await _productService.GetProductByIdAsync(id);

        if (result.IsSuccessful)
        {
            return Ok(result);
        }

        return StatusCode(result.Code, result);
    }



    [HttpPut("products/{id}")]
    public async Task<IActionResult> UpdateProduct(string id, [FromBody] UpdateProductDto updateDto)
    {
        var response = await _productService.UpdateProductAsync(id, updateDto);
        return StatusCode(response.Code, response);
    }

    

    [HttpDelete("product/{id}")]
    public async Task<IActionResult> DeleteProductById(string id)
    {
        var result = await _productService.DeleteProductAsync(id);

        if (result.IsSuccessful)
        {
            return Ok(result);
        }

        return StatusCode(result.Code, result);
    }

    [HttpGet("all-products")]
    public async Task<IActionResult> GetAllProducts()
    {
        var result = await _productService.GetAllProductsAsync();

        if (result.IsSuccessful)
        {
            return Ok(result);
        }

        return StatusCode(result.Code, result);
    }


    //Load page
    [HttpGet("products")]
    public async Task<IActionResult> GetProducts([FromQuery] string continuationToken = null, [FromQuery] int pageSize = 10)
    {
        var response = await _productService.GetProductsAsync(continuationToken, pageSize);
        return StatusCode(response.Code, response);
    }
}

