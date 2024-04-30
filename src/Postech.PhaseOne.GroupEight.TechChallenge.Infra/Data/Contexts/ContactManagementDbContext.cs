using Microsoft.EntityFrameworkCore;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Infra.Data.Contexts;

public class ContactManagementDbContext(DbContextOptions<ContactManagementDbContext> options) : DbContext(options)
{
    public DbSet<ContactEntity> Contacts { get; set; }
    public DbSet<ContactPhoneValueObject> ContactPhones { get; set; }
    public DbSet<RegionValueObject> Regions { get; set; }
    public DbSet<AreaCodeValueObject> AreaCodes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContactManagementDbContext).Assembly);
    }
}