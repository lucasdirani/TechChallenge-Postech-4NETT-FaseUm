using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Infra.Data.Configurations
{
    internal class ContactPhoneValueObjectConfiguration : IEntityTypeConfiguration<ContactPhoneValueObject>
    {
        public void Configure(EntityTypeBuilder<ContactPhoneValueObject> builder)
        {
            builder.ToTable("tb_contact_phone", "contacts");
            builder
                .Property<int>("ContactPhoneId")
                .HasColumnType("integer")
                .HasColumnName("contact_phone_id")
                .ValueGeneratedOnAdd();
            builder.HasKey("ContactPhoneId");
            builder
                .Property(c => c.Number)
                .HasColumnType("varchar")
                .HasMaxLength(9)
                .HasColumnName("contact_phone_number")
                .IsRequired();
            builder
                .Property<short>("AreaCodeId")
                .HasColumnType("smallint")
                .HasColumnName("contact_phone_area_code_id");
            builder
                .HasOne(a => a.AreaCode)
                .WithMany()
                .HasForeignKey("AreaCodeId")
                .IsRequired()
                .HasConstraintName("fk_tb_area_code_tb_contact_phone");
            builder
                .HasIndex("Number", "AreaCodeId")
                .HasDatabaseName("ix_tb_contact_phone_contact_phone_number_area_code_id") 
                .IsUnique();
            builder.Navigation(c => c.AreaCode).AutoInclude();
        }
    }
}