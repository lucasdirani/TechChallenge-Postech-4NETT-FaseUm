using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Infra.Data.Configurations
{
    internal class AreaCodeValueObjectConfiguration : IEntityTypeConfiguration<AreaCodeValueObject>
    {
        public void Configure(EntityTypeBuilder<AreaCodeValueObject> builder)
        {
            builder.ToTable("tb_area_code", "contacts");
            builder
                .Property<short>("AreaCodeId")
                .HasColumnType("smallint")
                .HasColumnName("area_code_id")
                .ValueGeneratedOnAdd();
            builder.HasKey("AreaCodeId");
            builder
                .Property(a => a.Value)
                .HasColumnType("char")
                .HasMaxLength(2)
                .HasColumnName("area_code_value")
                .IsFixedLength(true)
                .IsRequired();
            builder
                .Property<short>("RegionId")
                .HasColumnType("smallint")
                .HasColumnName("area_code_region_id");
            builder
                .HasOne(a => a.Region)
                .WithMany()
                .HasForeignKey("RegionId")
                .IsRequired()
                .HasConstraintName("fk_tb_region_tb_area_code");
            builder.HasIndex(a => a.Value).IsUnique().HasDatabaseName("ix_tb_area_code_area_code_value");
            builder.HasIndex("RegionId").HasDatabaseName("ix_tb_area_code_area_code_region_id");
            PopulateData(builder);
        }

        private static void PopulateData(EntityTypeBuilder<AreaCodeValueObject> builder)
        {
            builder.HasData(new[]
            {
                new { AreaCodeId = (short) 1, RegionId = (short) 25, Value = "11" },
                new { AreaCodeId = (short) 2, RegionId = (short) 25, Value = "12" },
                new { AreaCodeId = (short) 3, RegionId = (short) 25, Value = "13" },
                new { AreaCodeId = (short) 4, RegionId = (short) 25, Value = "14" },
                new { AreaCodeId = (short) 5, RegionId = (short) 25, Value = "15" },
                new { AreaCodeId = (short) 6, RegionId = (short) 25, Value = "16" },
                new { AreaCodeId = (short) 7, RegionId = (short) 25, Value = "17" },
                new { AreaCodeId = (short) 8, RegionId = (short) 25, Value = "18" },
                new { AreaCodeId = (short) 9, RegionId = (short) 25, Value = "19" },
                new { AreaCodeId = (short) 10, RegionId = (short) 19, Value = "21" },
                new { AreaCodeId = (short) 11, RegionId = (short) 19, Value = "22" },
                new { AreaCodeId = (short) 12, RegionId = (short) 19, Value = "24" },
                new { AreaCodeId = (short) 13, RegionId = (short) 8, Value = "27" },
                new { AreaCodeId = (short) 14, RegionId = (short) 8, Value = "28" },
                new { AreaCodeId = (short) 15, RegionId = (short) 13, Value = "31" },
                new { AreaCodeId = (short) 16, RegionId = (short) 13, Value = "32" },
                new { AreaCodeId = (short) 17, RegionId = (short) 13, Value = "33" },
                new { AreaCodeId = (short) 18, RegionId = (short) 13, Value = "34" },
                new { AreaCodeId = (short) 19, RegionId = (short) 13, Value = "35" },
                new { AreaCodeId = (short) 20, RegionId = (short) 13, Value = "37" },
                new { AreaCodeId = (short) 21, RegionId = (short) 13, Value = "38" },
                new { AreaCodeId = (short) 22, RegionId = (short) 16, Value = "41" },
                new { AreaCodeId = (short) 23, RegionId = (short) 16, Value = "42" },
                new { AreaCodeId = (short) 24, RegionId = (short) 16, Value = "43" },
                new { AreaCodeId = (short) 25, RegionId = (short) 16, Value = "44" },
                new { AreaCodeId = (short) 26, RegionId = (short) 16, Value = "45" },
                new { AreaCodeId = (short) 27, RegionId = (short) 16, Value = "46" },
                new { AreaCodeId = (short) 28, RegionId = (short) 24, Value = "47" },
                new { AreaCodeId = (short) 29, RegionId = (short) 24, Value = "48" },
                new { AreaCodeId = (short) 30, RegionId = (short) 24, Value = "49" },
                new { AreaCodeId = (short) 31, RegionId = (short) 21, Value = "51" },
                new { AreaCodeId = (short) 32, RegionId = (short) 21, Value = "53" },
                new { AreaCodeId = (short) 33, RegionId = (short) 21, Value = "54" },
                new { AreaCodeId = (short) 34, RegionId = (short) 21, Value = "55" },
                new { AreaCodeId = (short) 35, RegionId = (short) 7, Value = "61" },
                new { AreaCodeId = (short) 36, RegionId = (short) 9, Value = "62" },
                new { AreaCodeId = (short) 37, RegionId = (short) 27, Value = "63" },
                new { AreaCodeId = (short) 38, RegionId = (short) 9, Value = "64" },
                new { AreaCodeId = (short) 39, RegionId = (short) 11, Value = "65" },
                new { AreaCodeId = (short) 40, RegionId = (short) 11, Value = "66" },
                new { AreaCodeId = (short) 41, RegionId = (short) 12, Value = "67" },
                new { AreaCodeId = (short) 42, RegionId = (short) 1, Value = "68" },
                new { AreaCodeId = (short) 43, RegionId = (short) 22, Value = "69" },
                new { AreaCodeId = (short) 44, RegionId = (short) 5, Value = "71" },
                new { AreaCodeId = (short) 45, RegionId = (short) 5, Value = "73" },
                new { AreaCodeId = (short) 46, RegionId = (short) 5, Value = "74" },
                new { AreaCodeId = (short) 47, RegionId = (short) 5, Value = "75" },
                new { AreaCodeId = (short) 48, RegionId = (short) 5, Value = "77" },
                new { AreaCodeId = (short) 49, RegionId = (short) 26, Value = "79" },
                new { AreaCodeId = (short) 50, RegionId = (short) 17, Value = "81" },
                new { AreaCodeId = (short) 51, RegionId = (short) 2, Value = "82" },
                new { AreaCodeId = (short) 52, RegionId = (short) 15, Value = "83" },
                new { AreaCodeId = (short) 53, RegionId = (short) 20, Value = "84" },
                new { AreaCodeId = (short) 54, RegionId = (short) 6, Value = "85" },
                new { AreaCodeId = (short) 55, RegionId = (short) 18, Value = "86" },
                new { AreaCodeId = (short) 56, RegionId = (short) 17, Value = "87" },               
                new { AreaCodeId = (short) 57, RegionId = (short) 6, Value = "88" },      
                new { AreaCodeId = (short) 58, RegionId = (short) 18, Value = "89" },
                new { AreaCodeId = (short) 59, RegionId = (short) 14, Value = "91" },
                new { AreaCodeId = (short) 60, RegionId = (short) 4, Value = "92" },
                new { AreaCodeId = (short) 61, RegionId = (short) 14, Value = "93" },
                new { AreaCodeId = (short) 62, RegionId = (short) 14, Value = "94" },
                new { AreaCodeId = (short) 63, RegionId = (short) 23, Value = "95" },
                new { AreaCodeId = (short) 64, RegionId = (short) 3, Value = "96" },
                new { AreaCodeId = (short) 65, RegionId = (short) 4, Value = "97" },
                new { AreaCodeId = (short) 66, RegionId = (short) 10, Value = "98" },
                new { AreaCodeId = (short) 67, RegionId = (short) 10, Value = "99" },
            });
        }
    }
}