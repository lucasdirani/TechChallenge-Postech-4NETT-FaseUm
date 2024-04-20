using Microsoft.EntityFrameworkCore;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Infra;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<ContactEntity> Contacts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContactEntity>().HasKey(e => e.Id);
        modelBuilder.Entity<ContactEntity>().Property(e => e.Id).ValueGeneratedOnAdd();
    }
}
