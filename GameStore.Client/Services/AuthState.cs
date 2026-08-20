namespace GameStore.Client.Services;

public class AuthState
{
    private string? _token;

    public bool IsAuthenticated =>
        !string.IsNullOrWhiteSpace(_token);

    public string? Token => _token;

    public event Action? AuthenticationStateChanged;

    public void Login(string token)
    {
        _token = token;

        AuthenticationStateChanged?.Invoke();
    }

    public void Logout()
    {
        _token = null;

        AuthenticationStateChanged?.Invoke();
    }
}