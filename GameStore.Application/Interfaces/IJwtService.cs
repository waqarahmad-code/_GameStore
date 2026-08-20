namespace GameStore.Application.Interfaces;

public interface IJwtService
{
    string CreateToken(
        string userId,
        string userName,
        string email,
        IList<string> roles);
}