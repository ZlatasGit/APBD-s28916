namespace Data;
using Microsoft.EntityFrameworkCore;
using Models;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseSqlServer("Data Source=localhost, 1433; User=sa; Password=Password1.; Initial Catalog=Test; Integrated Security=False;Connect Timeout=30;Encrypt=False;")
            .LogTo(Console.WriteLine, LogLevel.Information);
    

    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Discount> Discounts { get; set; }

}
