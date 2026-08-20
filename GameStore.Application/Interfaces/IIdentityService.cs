using GameStore.Application.DTOs;

namespace GameStore.Application.Interfaces;

public interface IIdentityService
{
    Task<(bool Success, string[] Errors)> RegisterAsync(
        RegisterRequest request);

    Task<LoginResponse?> LoginAsync(
        LoginRequest request);
}