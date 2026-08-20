using GameStore.Client.Models;
using System.Net.Http.Json;


public class GameClient
{
    private readonly HttpClient _httpClient;

    public GameClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    //public async Task<Game[]?> GetGamesAsync()
    //{
    //    return await _httpClient.GetFromJsonAsync<Game[]>("api/games");
    //}

    public async Task<PagedResult<Game>> GetGamesAsync(
    string? search = null,
    int pageNumber = 1,
    int pageSize = 10)
    {
        var url =
            $"api/games?pageNumber={pageNumber}" +
            $"&pageSize={pageSize}";

        if (!string.IsNullOrWhiteSpace(search))
        {
            url += $"&search={Uri.EscapeDataString(search)}";
        }

        return await _httpClient.GetFromJsonAsync<PagedResult<Game>>(url)
               ?? new PagedResult<Game>();
    }

    public async Task<Game?> GetGameAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Game>(
            $"api/games/{id}");
    }

    public async Task AddGameAsync(Game game)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "api/games", game);

        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateGameAsync(Game game)
    {
        var response = await _httpClient.PutAsJsonAsync(
            $"api/games/{game.Id}", game);

        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteGameAsync(int id)
    {
        var response = await _httpClient.DeleteAsync(
            $"api/games/{id}");

        response.EnsureSuccessStatusCode();
    }
}