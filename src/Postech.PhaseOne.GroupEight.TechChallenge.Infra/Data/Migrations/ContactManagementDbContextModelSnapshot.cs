﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Postech.PhaseOne.GroupEight.TechChallenge.Infra.Data.Contexts;

#nullable disable

namespace Postech.PhaseOne.GroupEight.TechChallenge.Infra.Data.Migrations
{
    [DbContext(typeof(ContactManagementDbContext))]
    partial class ContactManagementDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities.ContactEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("contact_id");

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true)
                        .HasColumnName("contact_active");

                    b.Property<int>("ContactPhoneId")
                        .HasColumnType("integer")
                        .HasColumnName("contact_contact_phone_id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("contact_created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime?>("ModifiedAt")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("contact_modified_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("ContactPhoneId")
                        .HasDatabaseName("ix_tb_contact_contact_phone_id");

                    b.ToTable("tb_contact", "contacts");
                });

            modelBuilder.Entity("Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects.AreaCodeValueObject", b =>
                {
                    b.Property<short>("AreaCodeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasColumnName("area_code_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<short>("AreaCodeId"));

                    b.Property<short>("RegionId")
                        .HasColumnType("smallint")
                        .HasColumnName("area_code_region_id");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("char")
                        .HasColumnName("area_code_value")
                        .IsFixedLength();

                    b.HasKey("AreaCodeId");

                    b.HasIndex("RegionId")
                        .HasDatabaseName("ix_tb_area_code_area_code_region_id");

                    b.HasIndex("Value")
                        .IsUnique()
                        .HasDatabaseName("ix_tb_area_code_area_code_value");

                    b.ToTable("tb_area_code", "contacts");

                    b.HasData(
                        new
                        {
                            AreaCodeId = (short)1,
                            RegionId = (short)25,
                            Value = "11"
                        },
                        new
                        {
                            AreaCodeId = (short)2,
                            RegionId = (short)25,
                            Value = "12"
                        },
                        new
                        {
                            AreaCodeId = (short)3,
                            RegionId = (short)25,
                            Value = "13"
                        },
                        new
                        {
                            AreaCodeId = (short)4,
                            RegionId = (short)25,
                            Value = "14"
                        },
                        new
                        {
                            AreaCodeId = (short)5,
                            RegionId = (short)25,
                            Value = "15"
                        },
                        new
                        {
                            AreaCodeId = (short)6,
                            RegionId = (short)25,
                            Value = "16"
                        },
                        new
                        {
                            AreaCodeId = (short)7,
                            RegionId = (short)25,
                            Value = "17"
                        },
                        new
                        {
                            AreaCodeId = (short)8,
                            RegionId = (short)25,
                            Value = "18"
                        },
                        new
                        {
                            AreaCodeId = (short)9,
                            RegionId = (short)25,
                            Value = "19"
                        },
                        new
                        {
                            AreaCodeId = (short)10,
                            RegionId = (short)19,
                            Value = "21"
                        },
                        new
                        {
                            AreaCodeId = (short)11,
                            RegionId = (short)19,
                            Value = "22"
                        },
                        new
                        {
                            AreaCodeId = (short)12,
                            RegionId = (short)19,
                            Value = "24"
                        },
                        new
                        {
                            AreaCodeId = (short)13,
                            RegionId = (short)8,
                            Value = "27"
                        },
                        new
                        {
                            AreaCodeId = (short)14,
                            RegionId = (short)8,
                            Value = "28"
                        },
                        new
                        {
                            AreaCodeId = (short)15,
                            RegionId = (short)13,
                            Value = "31"
                        },
                        new
                        {
                            AreaCodeId = (short)16,
                            RegionId = (short)13,
                            Value = "32"
                        },
                        new
                        {
                            AreaCodeId = (short)17,
                            RegionId = (short)13,
                            Value = "33"
                        },
                        new
                        {
                            AreaCodeId = (short)18,
                            RegionId = (short)13,
                            Value = "34"
                        },
                        new
                        {
                            AreaCodeId = (short)19,
                            RegionId = (short)13,
                            Value = "35"
                        },
                        new
                        {
                            AreaCodeId = (short)20,
                            RegionId = (short)13,
                            Value = "37"
                        },
                        new
                        {
                            AreaCodeId = (short)21,
                            RegionId = (short)13,
                            Value = "38"
                        },
                        new
                        {
                            AreaCodeId = (short)22,
                            RegionId = (short)16,
                            Value = "41"
                        },
                        new
                        {
                            AreaCodeId = (short)23,
                            RegionId = (short)16,
                            Value = "42"
                        },
                        new
                        {
                            AreaCodeId = (short)24,
                            RegionId = (short)16,
                            Value = "43"
                        },
                        new
                        {
                            AreaCodeId = (short)25,
                            RegionId = (short)16,
                            Value = "44"
                        },
                        new
                        {
                            AreaCodeId = (short)26,
                            RegionId = (short)16,
                            Value = "45"
                        },
                        new
                        {
                            AreaCodeId = (short)27,
                            RegionId = (short)16,
                            Value = "46"
                        },
                        new
                        {
                            AreaCodeId = (short)28,
                            RegionId = (short)24,
                            Value = "47"
                        },
                        new
                        {
                            AreaCodeId = (short)29,
                            RegionId = (short)24,
                            Value = "48"
                        },
                        new
                        {
                            AreaCodeId = (short)30,
                            RegionId = (short)24,
                            Value = "49"
                        },
                        new
                        {
                            AreaCodeId = (short)31,
                            RegionId = (short)21,
                            Value = "51"
                        },
                        new
                        {
                            AreaCodeId = (short)32,
                            RegionId = (short)21,
                            Value = "53"
                        },
                        new
                        {
                            AreaCodeId = (short)33,
                            RegionId = (short)21,
                            Value = "54"
                        },
                        new
                        {
                            AreaCodeId = (short)34,
                            RegionId = (short)21,
                            Value = "55"
                        },
                        new
                        {
                            AreaCodeId = (short)35,
                            RegionId = (short)7,
                            Value = "61"
                        },
                        new
                        {
                            AreaCodeId = (short)36,
                            RegionId = (short)9,
                            Value = "62"
                        },
                        new
                        {
                            AreaCodeId = (short)37,
                            RegionId = (short)27,
                            Value = "63"
                        },
                        new
                        {
                            AreaCodeId = (short)38,
                            RegionId = (short)9,
                            Value = "64"
                        },
                        new
                        {
                            AreaCodeId = (short)39,
                            RegionId = (short)11,
                            Value = "65"
                        },
                        new
                        {
                            AreaCodeId = (short)40,
                            RegionId = (short)11,
                            Value = "66"
                        },
                        new
                        {
                            AreaCodeId = (short)41,
                            RegionId = (short)12,
                            Value = "67"
                        },
                        new
                        {
                            AreaCodeId = (short)42,
                            RegionId = (short)1,
                            Value = "68"
                        },
                        new
                        {
                            AreaCodeId = (short)43,
                            RegionId = (short)22,
                            Value = "69"
                        },
                        new
                        {
                            AreaCodeId = (short)44,
                            RegionId = (short)5,
                            Value = "71"
                        },
                        new
                        {
                            AreaCodeId = (short)45,
                            RegionId = (short)5,
                            Value = "73"
                        },
                        new
                        {
                            AreaCodeId = (short)46,
                            RegionId = (short)5,
                            Value = "74"
                        },
                        new
                        {
                            AreaCodeId = (short)47,
                            RegionId = (short)5,
                            Value = "75"
                        },
                        new
                        {
                            AreaCodeId = (short)48,
                            RegionId = (short)5,
                            Value = "77"
                        },
                        new
                        {
                            AreaCodeId = (short)49,
                            RegionId = (short)26,
                            Value = "79"
                        },
                        new
                        {
                            AreaCodeId = (short)50,
                            RegionId = (short)17,
                            Value = "81"
                        },
                        new
                        {
                            AreaCodeId = (short)51,
                            RegionId = (short)2,
                            Value = "82"
                        },
                        new
                        {
                            AreaCodeId = (short)52,
                            RegionId = (short)15,
                            Value = "83"
                        },
                        new
                        {
                            AreaCodeId = (short)53,
                            RegionId = (short)20,
                            Value = "84"
                        },
                        new
                        {
                            AreaCodeId = (short)54,
                            RegionId = (short)6,
                            Value = "85"
                        },
                        new
                        {
                            AreaCodeId = (short)55,
                            RegionId = (short)18,
                            Value = "86"
                        },
                        new
                        {
                            AreaCodeId = (short)56,
                            RegionId = (short)17,
                            Value = "87"
                        },
                        new
                        {
                            AreaCodeId = (short)57,
                            RegionId = (short)6,
                            Value = "88"
                        },
                        new
                        {
                            AreaCodeId = (short)58,
                            RegionId = (short)18,
                            Value = "89"
                        },
                        new
                        {
                            AreaCodeId = (short)59,
                            RegionId = (short)14,
                            Value = "91"
                        },
                        new
                        {
                            AreaCodeId = (short)60,
                            RegionId = (short)4,
                            Value = "92"
                        },
                        new
                        {
                            AreaCodeId = (short)61,
                            RegionId = (short)14,
                            Value = "93"
                        },
                        new
                        {
                            AreaCodeId = (short)62,
                            RegionId = (short)14,
                            Value = "94"
                        },
                        new
                        {
                            AreaCodeId = (short)63,
                            RegionId = (short)23,
                            Value = "95"
                        },
                        new
                        {
                            AreaCodeId = (short)64,
                            RegionId = (short)3,
                            Value = "96"
                        },
                        new
                        {
                            AreaCodeId = (short)65,
                            RegionId = (short)4,
                            Value = "97"
                        },
                        new
                        {
                            AreaCodeId = (short)66,
                            RegionId = (short)10,
                            Value = "98"
                        },
                        new
                        {
                            AreaCodeId = (short)67,
                            RegionId = (short)10,
                            Value = "99"
                        });
                });

            modelBuilder.Entity("Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects.ContactPhoneValueObject", b =>
                {
                    b.Property<int>("ContactPhoneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("contact_phone_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ContactPhoneId"));

                    b.Property<short>("AreaCodeId")
                        .HasColumnType("smallint")
                        .HasColumnName("contact_phone_area_code_id");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("varchar")
                        .HasColumnName("contact_phone_number");

                    b.HasKey("ContactPhoneId");

                    b.HasIndex("AreaCodeId")
                        .HasDatabaseName("ix_tb_contact_phone_area_code_id");

                    b.HasIndex("Number")
                        .IsUnique()
                        .HasDatabaseName("ix_tb_contact_phone_contact_phone_number");

                    b.ToTable("tb_contact_phone", "contacts");
                });

            modelBuilder.Entity("Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects.RegionValueObject", b =>
                {
                    b.Property<short>("RegionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasColumnName("region_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<short>("RegionId"));

                    b.Property<string>("RegionName")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("varchar")
                        .HasColumnName("region_name");

                    b.Property<string>("RegionStateName")
                        .IsRequired()
                        .HasMaxLength(19)
                        .HasColumnType("varchar")
                        .HasColumnName("region_state_name");

                    b.HasKey("RegionId");

                    b.ToTable("tb_region", "contacts");

                    b.HasData(
                        new
                        {
                            RegionId = (short)1,
                            RegionName = "Norte",
                            RegionStateName = "Acre"
                        },
                        new
                        {
                            RegionId = (short)2,
                            RegionName = "Nordeste",
                            RegionStateName = "Alagoas"
                        },
                        new
                        {
                            RegionId = (short)3,
                            RegionName = "Norte",
                            RegionStateName = "Amapá"
                        },
                        new
                        {
                            RegionId = (short)4,
                            RegionName = "Norte",
                            RegionStateName = "Amazonas"
                        },
                        new
                        {
                            RegionId = (short)5,
                            RegionName = "Nordeste",
                            RegionStateName = "Bahia"
                        },
                        new
                        {
                            RegionId = (short)6,
                            RegionName = "Nordeste",
                            RegionStateName = "Ceará"
                        },
                        new
                        {
                            RegionId = (short)7,
                            RegionName = "Centro-Oeste",
                            RegionStateName = "Distrito Federal"
                        },
                        new
                        {
                            RegionId = (short)8,
                            RegionName = "Sudeste",
                            RegionStateName = "Espírito Santo"
                        },
                        new
                        {
                            RegionId = (short)9,
                            RegionName = "Centro-Oeste",
                            RegionStateName = "Goiás"
                        },
                        new
                        {
                            RegionId = (short)10,
                            RegionName = "Nordeste",
                            RegionStateName = "Maranhão"
                        },
                        new
                        {
                            RegionId = (short)11,
                            RegionName = "Centro-Oeste",
                            RegionStateName = "Mato Grosso"
                        },
                        new
                        {
                            RegionId = (short)12,
                            RegionName = "Centro-Oeste",
                            RegionStateName = "Mato Grosso do Sul"
                        },
                        new
                        {
                            RegionId = (short)13,
                            RegionName = "Sudeste",
                            RegionStateName = "Minas Gerais"
                        },
                        new
                        {
                            RegionId = (short)14,
                            RegionName = "Norte",
                            RegionStateName = "Pará"
                        },
                        new
                        {
                            RegionId = (short)15,
                            RegionName = "Nordeste",
                            RegionStateName = "Paraíba"
                        },
                        new
                        {
                            RegionId = (short)16,
                            RegionName = "Sul",
                            RegionStateName = "Paraná"
                        },
                        new
                        {
                            RegionId = (short)17,
                            RegionName = "Nordeste",
                            RegionStateName = "Pernambuco"
                        },
                        new
                        {
                            RegionId = (short)18,
                            RegionName = "Nordeste",
                            RegionStateName = "Piauí"
                        },
                        new
                        {
                            RegionId = (short)19,
                            RegionName = "Sudeste",
                            RegionStateName = "Rio de Janeiro"
                        },
                        new
                        {
                            RegionId = (short)20,
                            RegionName = "Nordeste",
                            RegionStateName = "Rio Grande do Norte"
                        },
                        new
                        {
                            RegionId = (short)21,
                            RegionName = "Sul",
                            RegionStateName = "Rio Grande do Sul"
                        },
                        new
                        {
                            RegionId = (short)22,
                            RegionName = "Norte",
                            RegionStateName = "Rondônia"
                        },
                        new
                        {
                            RegionId = (short)23,
                            RegionName = "Norte",
                            RegionStateName = "Roraima"
                        },
                        new
                        {
                            RegionId = (short)24,
                            RegionName = "Sul",
                            RegionStateName = "Santa Catarina"
                        },
                        new
                        {
                            RegionId = (short)25,
                            RegionName = "Sudeste",
                            RegionStateName = "São Paulo"
                        },
                        new
                        {
                            RegionId = (short)26,
                            RegionName = "Nordeste",
                            RegionStateName = "Sergipe"
                        },
                        new
                        {
                            RegionId = (short)27,
                            RegionName = "Norte",
                            RegionStateName = "Tocantins"
                        });
                });

            modelBuilder.Entity("Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities.ContactEntity", b =>
                {
                    b.HasOne("Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects.ContactPhoneValueObject", "ContactPhone")
                        .WithMany()
                        .HasForeignKey("ContactPhoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tb_contact_phone_tb_contact");

                    b.OwnsOne("Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects.ContactEmailValueObject", "ContactEmail", b1 =>
                        {
                            b1.Property<Guid>("ContactEntityId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(60)
                                .HasColumnType("varchar")
                                .HasColumnName("contact_email");

                            b1.HasKey("ContactEntityId");

                            b1.ToTable("tb_contact", "contacts");

                            b1.WithOwner()
                                .HasForeignKey("ContactEntityId");
                        });

                    b.OwnsOne("Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects.ContactNameValueObject", "ContactName", b1 =>
                        {
                            b1.Property<Guid>("ContactEntityId")
                                .HasColumnType("uuid");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(40)
                                .HasColumnType("varchar")
                                .HasColumnName("contact_first_name");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(60)
                                .HasColumnType("varchar")
                                .HasColumnName("contact_last_name");

                            b1.HasKey("ContactEntityId");

                            b1.ToTable("tb_contact", "contacts");

                            b1.WithOwner()
                                .HasForeignKey("ContactEntityId");
                        });

                    b.Navigation("ContactEmail")
                        .IsRequired();

                    b.Navigation("ContactName")
                        .IsRequired();

                    b.Navigation("ContactPhone");
                });

            modelBuilder.Entity("Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects.AreaCodeValueObject", b =>
                {
                    b.HasOne("Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects.RegionValueObject", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tb_region_tb_area_code");

                    b.Navigation("Region");
                });

            modelBuilder.Entity("Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects.ContactPhoneValueObject", b =>
                {
                    b.HasOne("Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects.AreaCodeValueObject", "AreaCode")
                        .WithMany()
                        .HasForeignKey("AreaCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tb_area_code_tb_contact_phone");

                    b.Navigation("AreaCode");
                });
#pragma warning restore 612, 618
        }
    }
}
