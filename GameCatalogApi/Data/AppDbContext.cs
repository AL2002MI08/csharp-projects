using Microsoft.EntityFrameworkCore;
using GameCatalogApi.Models;

namespace GameCatalogApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Game> Games => Set<Game>();
    public DbSet<Review> Reviews => Set<Review>();
}
