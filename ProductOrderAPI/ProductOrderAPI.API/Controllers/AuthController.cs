using Microsoft.AspNetCore.Mvc;
using ProductOrderAPI.Application.DTOs;
using ProductOrderAPI.Domain.Entities;
using ProductOrderAPI.Infrastructure.Data;
using ProductOrderAPI.Infrastructure.Services;
using System.Security.Cryptography;
using System.Text;

namespace ProductOrderAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;
    private readonly StoreDbContext _context;

    public AuthController(JwtService jwtService, StoreDbContext context)
    {
        _jwtService = jwtService;
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
            return BadRequest("მომხმარებელი და პაროლი სავალდებულოა");

        if (_context.Users.Any(u => u.Username == dto.Username))
            return BadRequest("მომხმარებელი უკვე არსებობს");

        if (dto.Role != "admin" && dto.Role != "user")
            return BadRequest("როლი უნდა იყოს admin ან user");

        var user = new User
        {
            Username = dto.Username,
            PasswordHash = ComputeHash(dto.Password),
            Role = dto.Role
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return Ok("რეგისტრაცია წარმატებით დასრულდა");
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == dto.Username);
        if (user == null)
            return Unauthorized("მომხმარებელი არ მოიძებნა");

        if (user.PasswordHash != ComputeHash(dto.Password))
            return Unauthorized("პაროლი არასწორია");

        var token = _jwtService.GenerateToken(user.Username, user.Role);
        return Ok(new { token });
    }

    private string ComputeHash(string input)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(bytes);
    }
}