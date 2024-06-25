namespace PROJECT.Context;
using Microsoft.EntityFrameworkCore;
using Models;
using Services;

public class SystemDbContext : DbContext
{
    public SystemDbContext() { }
    public SystemDbContext(DbContextOptions<SystemDbContext> options) : base(options) 
    { 
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseSqlServer("Data Source=localhost, 1433; User=sa; Password=Password1.; Initial Catalog=project; Integrated Security=False;Connect Timeout=30;Encrypt=False;")
            .LogTo(Console.WriteLine, LogLevel.Information);
    
    public DbSet<IndividualClient> IndividualClients { get; set; }
    public DbSet<CorporateClient> CorporateClients { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<SoftwareSystem> SoftwareSystems { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Version> Versions { get; set; }
}