using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductOrderAPI.Application.DTOs;
using ProductOrderAPI.Application.Services;

namespace ProductOrderAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] int? categoryId,
        [FromQuery] string? sortBy,
        [FromQuery] string? sortDirection)
    {
        var result = await _productService.GetAllAsync(minPrice, maxPrice, categoryId, sortBy, sortDirection);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _productService.GetByIdAsync(id);
        if (result == null) return NotFound("პროდუქტი არ მოიძებნა");
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create(ProductCreateDto dto)
    {
        var error = await _productService.CreateAsync(dto);
        if (error != null) return BadRequest(error);
        return Ok("პროდუქტი შეიქმნა");
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Update(int id, ProductCreateDto dto)
    {
        var error = await _productService.UpdateAsync(id, dto);
        if (error != null) return BadRequest(error);
        return Ok("პროდუქტი განახლდა");
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var error = await _productService.DeleteAsync(id);
        if (error != null) return BadRequest(error);
        return Ok("პროდუქტი წაიშალა");
    }
}