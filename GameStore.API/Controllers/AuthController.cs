using GameStore.Application.DTOs;
using GameStore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IIdentityService _identityService;

    public AuthController(
        IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterRequest request)
    {
        var result =
            await _identityService.RegisterAsync(request);

        if (!result.Success)
        {
            return BadRequest(new
            {
                errors = result.Errors
            });
        }

        return Ok(new
        {
            message = "User registered successfully."
        });
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(
        LoginRequest request)
    {
        var result =
            await _identityService.LoginAsync(request);

        if (result == null)
        {
            return Unauthorized(
                "Invalid email or password.");
        }

        return Ok(result);
    }
}