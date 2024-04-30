using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Enumerators;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;
using Postech.PhaseOne.GroupEight.TechChallenge.Infra.Data.Converters;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Infra.Data.Configurations
{
    internal class RegionValueObjectConfiguration : IEntityTypeConfiguration<RegionValueObject>
    {
        public void Configure(EntityTypeBuilder<RegionValueObject> builder)
        {
            builder.ToTable("tb_region", "contacts");
            builder
                .Property<short>("RegionId")
                .HasColumnType("smallint")
                .HasColumnName("region_id")
                .ValueGeneratedOnAdd();
            builder.HasKey("RegionId");
            builder
                .Property(r => r.RegionName)
                .HasConversion(new EnumDescriptionToStringConverter<RegionNameEnumerator>())
                .HasColumnType("varchar")
                .HasMaxLength(12)
                .HasColumnName("region_name")
                .IsRequired();
            builder
                .Property(r => r.RegionStateName)
                .HasConversion(new EnumDescriptionToStringConverter<RegionStateNameEnumerator>())
                .HasColumnType("varchar")
                .HasMaxLength(19)
                .HasColumnName("region_state_name")
                .IsRequired();
            PopulateData(builder);
        }

        private static void PopulateData(EntityTypeBuilder<RegionValueObject> builder)
        {
            builder.HasData(new[]
            {
                new
                {
                    RegionId = (short) 1,
                    RegionName = RegionNameEnumerator.North,
                    RegionStateName = RegionStateNameEnumerator.Acre
                },
                new
                {
                    RegionId = (short) 2,
                    RegionName = RegionNameEnumerator.Northeast,
                    RegionStateName = RegionStateNameEnumerator.Alagoas
                },
                new
                {
                    RegionId = (short) 3,
                    RegionName = RegionNameEnumerator.North,
                    RegionStateName = RegionStateNameEnumerator.Amapa
                },
                new
                {
                    RegionId = (short) 4,
                    RegionName = RegionNameEnumerator.North,
                    RegionStateName = RegionStateNameEnumerator.Amazonas
                },
                new
                {
                    RegionId = (short) 5,
                    RegionName = RegionNameEnumerator.Northeast,
                    RegionStateName = RegionStateNameEnumerator.Bahia
                },
                new
                {
                    RegionId = (short) 6,
                    RegionName = RegionNameEnumerator.Northeast,
                    RegionStateName = RegionStateNameEnumerator.Ceara
                },
                new 
                {
                    RegionId = (short) 7, 
                    RegionName = RegionNameEnumerator.Midwest,
                    RegionStateName = RegionStateNameEnumerator.DistritoFederal
                },
                new 
                {
                    RegionId = (short) 8, 
                    RegionName = RegionNameEnumerator.Southeast, 
                    RegionStateName = RegionStateNameEnumerator.EspiritoSanto
                },
                new 
                {
                    RegionId = (short) 9, 
                    RegionName = RegionNameEnumerator.Midwest, 
                    RegionStateName = RegionStateNameEnumerator.Goias
                },
                new 
                {
                    RegionId = (short) 10, 
                    RegionName = RegionNameEnumerator.Northeast,
                    RegionStateName = RegionStateNameEnumerator.Maranhao
                },
                new 
                {
                    RegionId = (short) 11, 
                    RegionName = RegionNameEnumerator.Midwest,
                    RegionStateName = RegionStateNameEnumerator.MatoGrosso
                },
                new
                {
                    RegionId = (short) 12,
                    RegionName = RegionNameEnumerator.Midwest,
                    RegionStateName = RegionStateNameEnumerator.MatoGrossoDoSul
                },
                new 
                {
                    RegionId = (short) 13,
                    RegionName = RegionNameEnumerator.Southeast, 
                    RegionStateName = RegionStateNameEnumerator.MinasGerais
                },
                new
                {
                    RegionId = (short) 14,
                    RegionName = RegionNameEnumerator.North,
                    RegionStateName = RegionStateNameEnumerator.Para
                },
                new
                {
                    RegionId = (short) 15,
                    RegionName = RegionNameEnumerator.Northeast,
                    RegionStateName = RegionStateNameEnumerator.Paraiba
                },
                new
                {
                    RegionId = (short) 16,
                    RegionName = RegionNameEnumerator.South,
                    RegionStateName = RegionStateNameEnumerator.Parana
                },
                new
                {
                    RegionId = (short) 17,
                    RegionName = RegionNameEnumerator.Northeast,
                    RegionStateName = RegionStateNameEnumerator.Pernambuco
                },
                new
                {
                    RegionId = (short) 18,
                    RegionName = RegionNameEnumerator.Northeast,
                    RegionStateName = RegionStateNameEnumerator.Piaui
                },
                new
                {
                    RegionId = (short) 19,
                    RegionName = RegionNameEnumerator.Southeast,
                    RegionStateName = RegionStateNameEnumerator.RioDeJaneiro
                },
                new
                {
                    RegionId = (short) 20,
                    RegionName = RegionNameEnumerator.Northeast,
                    RegionStateName = RegionStateNameEnumerator.RioGrandeDoNorte
                },
                new
                {
                    RegionId = (short) 21,
                    RegionName = RegionNameEnumerator.South,
                    RegionStateName = RegionStateNameEnumerator.RioGrandeDoSul
                },
                new
                {
                    RegionId = (short) 22,
                    RegionName = RegionNameEnumerator.North,
                    RegionStateName = RegionStateNameEnumerator.Rondonia
                },
                new
                {
                    RegionId = (short) 23,
                    RegionName = RegionNameEnumerator.North,
                    RegionStateName = RegionStateNameEnumerator.Roraima
                },
                new
                {
                    RegionId = (short) 24,
                    RegionName = RegionNameEnumerator.South,
                    RegionStateName = RegionStateNameEnumerator.SantaCatarina
                },
                new
                {
                    RegionId = (short) 25,
                    RegionName = RegionNameEnumerator.Southeast,
                    RegionStateName = RegionStateNameEnumerator.SaoPaulo
                },
                new
                {
                    RegionId = (short) 26,
                    RegionName = RegionNameEnumerator.Northeast,
                    RegionStateName = RegionStateNameEnumerator.Sergipe
                },
                new
                {
                    RegionId = (short)27,
                    RegionName = RegionNameEnumerator.North,
                    RegionStateName = RegionStateNameEnumerator.Tocantins
                },
            });
        }
    }
}