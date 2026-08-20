using GameStore.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infrastructure.Persistence;

public class GameDbContext
    : IdentityDbContext<ApplicationUser>
{
    public GameDbContext(
        DbContextOptions<GameDbContext> options)
        : base(options)
    {
    }

    public DbSet<Game> Games => Set<Game>();
}