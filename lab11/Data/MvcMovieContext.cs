using Microsoft.EntityFrameworkCore;
using lab11.Models;

namespace lab11.Data;

public class MvcMovieContext : DbContext
{
    public MvcMovieContext (DbContextOptions<MvcMovieContext> options) : base(options)
    {
    }

    public DbSet<Movie> Movie { get; set; } = default!;
}
