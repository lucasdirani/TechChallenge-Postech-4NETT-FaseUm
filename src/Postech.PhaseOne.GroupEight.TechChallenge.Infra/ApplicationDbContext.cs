using Microsoft.EntityFrameworkCore;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Infra;

public class ApplicationDbContext : DbContext
{
    private readonly string _connectionString;
    public ApplicationDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbSet<ContactEntity> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContactEntity>(e =>
        {
            e.ToTable("Contact");
            e.HasKey(p => p.Id);
            e.Property(p => p.Id).HasColumnType("INT").ValueGeneratedNever().UseIdentityColumn();
            e.Property(p => p.CreatedAt).HasColumnName("CreatedAt").IsRequired();
        });
            
            
    }
}
