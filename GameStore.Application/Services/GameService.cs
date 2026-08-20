using GameStore.Application.DTOs;
using GameStore.Application.Interfaces;
using GameStore.Domain.Entities;

namespace GameStore.Application.Services;

public class GameService
{
    private readonly IGameRepository _repository;

    public GameService(IGameRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<GameDto>> SearchAsync(
        string? search,
        int pageNumber,
        int pageSize)
    {
        if (pageNumber < 1)
            pageNumber = 1;

        if (pageSize < 1)
            pageSize = 10;

        if (pageSize > 100)
            pageSize = 100;

        var result = await _repository.SearchAsync(
            search,
            pageNumber,
            pageSize);

        var items = result.Items
            .Select(game => MapToDto(game))
            .ToList();

        return new PagedResult<GameDto>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = result.TotalItems,
            TotalPages = (int)Math.Ceiling(
                result.TotalItems / (double)pageSize)
        };
    }

    public async Task<GameDto?> GetByIdAsync(int id)
    {
        var game = await _repository.GetByIdAsync(id);

        if (game == null)
            return null;

        return MapToDto(game);
    }

    public async Task<GameDto> CreateAsync(
        CreateGameRequest request)
    {
        var game = new Game
        {
            Name = request.Name,
            Genre = request.Genre,
            Price = request.Price,
            ReleaseDate = request.ReleaseDate
        };

        var result = await _repository.AddAsync(game);

        return MapToDto(result);
    }

    public async Task<GameDto?> UpdateAsync(
        int id,
        UpdateGameRequest request)
    {
        var game = await _repository.GetByIdAsync(id);

        if (game == null)
            return null;

        game.Name = request.Name;
        game.Genre = request.Genre;
        game.Price = request.Price;
        game.ReleaseDate = request.ReleaseDate;

        await _repository.UpdateAsync(game);

        return MapToDto(game);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    private static GameDto MapToDto(Game game)
    {
        return new GameDto
        {
            Id = game.Id,
            Name = game.Name,
            Genre = game.Genre,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }
}