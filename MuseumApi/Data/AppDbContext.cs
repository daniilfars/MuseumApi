using Microsoft.EntityFrameworkCore;
using MuseumApi.Models;

namespace MuseumApi.Data;

public class AppDbContext : DbContext
{
    public DbSet<Painting> Paintings { get; set; } = null!;
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Painting>().HasData(
            new Painting { Id = 1, Title = "Mono", Author = "Da Vinchi", Year = 1872 },
            new Painting { Id = 2, Title = "Football", Author = "Lionel Messi", Year = 2014 }
        );
    }
}
