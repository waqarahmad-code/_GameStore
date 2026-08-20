using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.Application.Interfaces
{
    public interface IGameRepository
    {
        Task<(IEnumerable<Game> Items, int TotalItems)> SearchAsync(
            string? search,
            int pageNumber,
            int pageSize);

        Task<Game?> GetByIdAsync(int id);

        Task<Game> AddAsync(Game game);

        Task UpdateAsync(Game game);

        Task DeleteAsync(int id);
    }
}
