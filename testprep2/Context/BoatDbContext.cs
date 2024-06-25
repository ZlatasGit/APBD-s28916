namespace Context;
using Microsoft.EntityFrameworkCore;
using Models;

public class BoatDbContext : DbContext
{
    public BoatDbContext() { }
    public BoatDbContext(DbContextOptions<BoatDbContext> options) : base(options) 
    { 
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseSqlServer("Data Source=localhost, 1433; User=sa; Password=Password1.; Initial Catalog=boats; Integrated Security=False;Connect Timeout=30;Encrypt=False;")
            .LogTo(Console.WriteLine, LogLevel.Information);

    public DbSet<Sailboat> Sailboats { get; set; }
    public DbSet<ClientCategory> ClientCategories { get; set; }
    public DbSet<BoatStandard> BoatStandards { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Client> Clients { get; set; }

}