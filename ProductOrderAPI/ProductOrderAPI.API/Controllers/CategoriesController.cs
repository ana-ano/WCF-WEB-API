using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductOrderAPI.Application.DTOs;
using ProductOrderAPI.Domain.Entities;
using ProductOrderAPI.Domain.Interfaces;

namespace ProductOrderAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IRepository<Category> _categoryRepo;

    public CategoriesController(IRepository<Category> categoryRepo)
    {
        _categoryRepo = categoryRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryRepo.GetAllAsync();
        var result = categories.Select(c => new CategoryReadDto
        {
            Id = c.Id,
            Name = c.Name
        });
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _categoryRepo.GetByIdAsync(id);
        if (category == null) return NotFound("კატეგორია არ მოიძებნა");

        return Ok(new CategoryReadDto { Id = category.Id, Name = category.Name });
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create(CategoryCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return BadRequest("სახელი სავალდებულოა");

        var category = new Category { Name = dto.Name };
        await _categoryRepo.AddAsync(category);
        return Ok("კატეგორია შეიქმნა");
    }
}