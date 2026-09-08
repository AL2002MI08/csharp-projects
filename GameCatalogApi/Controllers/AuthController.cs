using Microsoft.AspNetCore.Mvc;
using GameCatalogApi.Models;
using GameCatalogApi.Services;
using System;
using System.Collections.Generic;

namespace GameCatalogApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;

    public AuthController(TokenService tokenService) => _tokenService = tokenService;

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var validUsers = new Dictionary<string, (string Password, string Role)>(StringComparer.OrdinalIgnoreCase)
        {
            { "admin@gmail.com", ("Admin123!", "Admin") },
            { "dev@gmail.com",   ("Developer123!",   "Developer") },
            { "gamer@gmail.com", ("Gamer123!", "Gamer") }
        };

        if (!validUsers.TryGetValue(request.Email, out var user) || request.Password != user.Password)
            return Unauthorized("Invalid email or password");

        var token = _tokenService.GenerateToken(request.Email, user.Role);
        return Ok(new { token });
    }
}
