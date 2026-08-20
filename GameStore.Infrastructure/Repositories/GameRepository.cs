using GameStore.Application.Interfaces;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infrastructure.Repositories;

public class GameRepository : IGameRepository
{
    private readonly GameDbContext _context;

    public GameRepository(GameDbContext context)
    {
        _context = context;
    }

    public async Task<(IEnumerable<Game> Items, int TotalItems)> SearchAsync(
        string? search,
        int pageNumber,
        int pageSize)
    {
        var query = _context.Games
            .AsNoTracking()
            .AsQueryable();

        // Search
        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();

            query = query.Where(g =>
                g.Name.Contains(search) ||
                g.Genre.Contains(search));
        }

        // Total records before paging
        var totalItems = await query.CountAsync();

        // Paging
        var items = await query
            .OrderBy(g => g.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalItems);
    }

    public async Task<Game?> GetByIdAsync(int id)
    {
        return await _context.Games
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<Game> AddAsync(Game game)
    {
        _context.Games.Add(game);

        await _context.SaveChangesAsync();

        return game;
    }

    public async Task UpdateAsync(Game game)
    {
        _context.Games.Update(game);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var game = await _context.Games
            .FirstOrDefaultAsync(g => g.Id == id);

        if (game == null)
        {
            return;
        }

        _context.Games.Remove(game);

        await _context.SaveChangesAsync();
    }
}