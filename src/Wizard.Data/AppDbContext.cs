using Microsoft.EntityFrameworkCore;
using Wizard.Domain.Entities;

namespace Wizard.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Country> Countries { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<RegistrationInfo> RegistrationInfos { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RegistrationInfo>()
            .HasKey(c => c.Email);
    }
}