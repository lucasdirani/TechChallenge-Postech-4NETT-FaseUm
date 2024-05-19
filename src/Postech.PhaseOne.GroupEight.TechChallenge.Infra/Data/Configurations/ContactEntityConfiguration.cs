using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Infra.Data.Configurations
{
    internal class ContactEntityConfiguration : IEntityTypeConfiguration<ContactEntity>
    {
        public void Configure(EntityTypeBuilder<ContactEntity> builder)
        {
            builder.ToTable("tb_contact", "contacts");
            builder
                .Property(c => c.Id)
                .HasColumnName("contact_id")
                .HasColumnType("uuid")
                .IsRequired();
            builder.HasKey(c => c.Id);
            builder
                .Property(c => c.Active)
                .HasColumnName("contact_active")
                .HasColumnType("boolean")
                .HasDefaultValue(true)
                .IsRequired();
            builder
                .Property(c => c.CreatedAt)
                .HasColumnName("contact_created_at")
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();
            builder
                .OwnsOne(c => c.ContactName, contactName =>
                {
                    contactName
                        .Property(c => c.FirstName)
                        .HasColumnName("contact_first_name")
                        .HasColumnType("varchar")
                        .HasMaxLength(40)
                        .IsRequired();
                    contactName
                        .Property(c => c.LastName)
                        .HasColumnName("contact_last_name")
                        .HasColumnType("varchar")
                        .HasMaxLength(60)
                        .IsRequired();
                });
            builder
                .OwnsOne(c => c.ContactEmail, contactEmail =>
                {
                    contactEmail
                        .Property(c => c.Value)
                        .HasColumnName("contact_email")
                        .HasColumnType("varchar")
                        .HasMaxLength(60)
                        .IsRequired();
                });
            builder
                .Property(c => c.ModifiedAt)
                .HasColumnName("contact_modified_at")
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();
            builder
                .Property<int>("ContactPhoneId")
                .HasColumnType("integer")
                .HasColumnName("contact_contact_phone_id");
            builder
                .HasOne(a => a.ContactPhone)
                .WithMany()
                .HasForeignKey("ContactPhoneId")
                .IsRequired()
                .HasConstraintName("fk_tb_contact_phone_tb_contact");
            builder.HasIndex("ContactPhoneId").HasDatabaseName("ix_tb_contact_contact_phone_id");
            builder.Navigation(c => c.ContactPhone).AutoInclude();
        }
    }
}