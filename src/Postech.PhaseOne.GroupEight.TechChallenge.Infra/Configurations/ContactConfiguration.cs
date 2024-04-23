using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using System.Reflection.Emit;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Infra.Configurations;

public class ContactConfiguration : IEntityTypeConfiguration<ContactEntity>
{
    public void Configure(EntityTypeBuilder<ContactEntity> builder)
    {
        builder.ToTable("Contact");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnType("INT").ValueGeneratedNever().UseIdentityColumn();
        builder.Property(p => p.CreatedAt).HasColumnName("CreatedAt").IsRequired();
    }
}
