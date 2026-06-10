using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductOrderAPI.Application.DTOs;
using ProductOrderAPI.Application.Services;

namespace ProductOrderAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrdersController(OrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] DateTime? orderDateFrom = null,
        [FromQuery] DateTime? orderDateTo = null)
    {
        var result = await _orderService.GetAllAsync(pageNumber, pageSize, orderDateFrom, orderDateTo);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _orderService.GetByIdAsync(id);
        if (result == null) return NotFound("შეკვეთა არ მოიძებნა");
        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(OrderCreateDto dto)
    {
        var error = await _orderService.CreateAsync(dto);
        if (error != null) return BadRequest(error);
        return Ok("შეკვეთა შეიქმნა");
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, OrderCreateDto dto)
    {
        var error = await _orderService.UpdateAsync(id, dto);
        if (error != null) return BadRequest(error);
        return Ok("შეკვეთა განახლდა");
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var error = await _orderService.DeleteAsync(id);
        if (error != null) return BadRequest(error);
        return Ok("შეკვეთა წაიშალა");
    }
}