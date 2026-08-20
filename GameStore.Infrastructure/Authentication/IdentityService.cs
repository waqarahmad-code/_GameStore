using GameStore.Application.DTOs;
using GameStore.Application.Interfaces;
using GameStore.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Infrastructure.Authentication;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtService _jwtService;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        IJwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<(bool Success, string[] Errors)> RegisterAsync(
        RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var result = await _userManager.CreateAsync(
            user,
            request.Password);

        if (!result.Succeeded)
        {
            return (
                false,
                result.Errors
                    .Select(e => e.Description)
                    .ToArray());
        }

        return (true, Array.Empty<string>());
    }

    public async Task<LoginResponse?> LoginAsync(
        LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(
            request.Email);

        if (user == null)
        {
            return null;
        }

        var passwordValid =
            await _userManager.CheckPasswordAsync(
                user,
                request.Password);

        if (!passwordValid)
        {
            return null;
        }

        var roles =
            await _userManager.GetRolesAsync(user);

        var token = _jwtService.CreateToken(
            user.Id,
            user.UserName ?? user.Email!,
            user.Email!,
            roles);

        var expiresAt =
            DateTime.UtcNow.AddMinutes(60);

        return new LoginResponse
        {
            Token = token,
            ExpiresAt = expiresAt,
            UserId = user.Id,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }
}