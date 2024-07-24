using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace Postech.PhaseOne.GroupEight.TechChallenge.Infra.Data.Migrations
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "contacts");

            migrationBuilder.CreateTable(
                name: "tb_region",
                schema: "contacts",
                columns: table => new
                {
                    region_id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    region_name = table.Column<string>(type: "varchar", maxLength: 12, nullable: false),
                    region_state_name = table.Column<string>(type: "varchar", maxLength: 19, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tb_region", x => x.region_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_area_code",
                schema: "contacts",
                columns: table => new
                {
                    area_code_id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    area_code_value = table.Column<string>(type: "char(2)", fixedLength: true, maxLength: 2, nullable: false),
                    area_code_region_id = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tb_area_code", x => x.area_code_id);
                    table.ForeignKey(
                        name: "fk_tb_region_tb_area_code",
                        column: x => x.area_code_region_id,
                        principalSchema: "contacts",
                        principalTable: "tb_region",
                        principalColumn: "region_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_contact_phone",
                schema: "contacts",
                columns: table => new
                {
                    contact_phone_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    contact_phone_number = table.Column<string>(type: "varchar", maxLength: 9, nullable: false),
                    contact_phone_area_code_id = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tb_contact_phone", x => x.contact_phone_id);
                    table.ForeignKey(
                        name: "fk_tb_area_code_tb_contact_phone",
                        column: x => x.contact_phone_area_code_id,
                        principalSchema: "contacts",
                        principalTable: "tb_area_code",
                        principalColumn: "area_code_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_contact",
                schema: "contacts",
                columns: table => new
                {
                    contact_id = table.Column<Guid>(type: "uuid", nullable: false),
                    contact_first_name = table.Column<string>(type: "varchar", maxLength: 40, nullable: false),
                    contact_last_name = table.Column<string>(type: "varchar", maxLength: 60, nullable: false),
                    contact_email = table.Column<string>(type: "varchar", maxLength: 60, nullable: false),
                    contact_contact_phone_id = table.Column<int>(type: "integer", nullable: false),
                    contact_created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    contact_modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    contact_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tb_contact", x => x.contact_id);
                    table.ForeignKey(
                        name: "fk_tb_contact_phone_tb_contact",
                        column: x => x.contact_contact_phone_id,
                        principalSchema: "contacts",
                        principalTable: "tb_contact_phone",
                        principalColumn: "contact_phone_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "contacts",
                table: "tb_region",
                columns: ["region_id", "region_name", "region_state_name"],
                values: new object[,]
                {
                    { (short)1, "Norte", "Acre" },
                    { (short)2, "Nordeste", "Alagoas" },
                    { (short)3, "Norte", "Amapá" },
                    { (short)4, "Norte", "Amazonas" },
                    { (short)5, "Nordeste", "Bahia" },
                    { (short)6, "Nordeste", "Ceará" },
                    { (short)7, "Centro-Oeste", "Distrito Federal" },
                    { (short)8, "Sudeste", "Espírito Santo" },
                    { (short)9, "Centro-Oeste", "Goiás" },
                    { (short)10, "Nordeste", "Maranhão" },
                    { (short)11, "Centro-Oeste", "Mato Grosso" },
                    { (short)12, "Centro-Oeste", "Mato Grosso do Sul" },
                    { (short)13, "Sudeste", "Minas Gerais" },
                    { (short)14, "Norte", "Pará" },
                    { (short)15, "Nordeste", "Paraíba" },
                    { (short)16, "Sul", "Paraná" },
                    { (short)17, "Nordeste", "Pernambuco" },
                    { (short)18, "Nordeste", "Piauí" },
                    { (short)19, "Sudeste", "Rio de Janeiro" },
                    { (short)20, "Nordeste", "Rio Grande do Norte" },
                    { (short)21, "Sul", "Rio Grande do Sul" },
                    { (short)22, "Norte", "Rondônia" },
                    { (short)23, "Norte", "Roraima" },
                    { (short)24, "Sul", "Santa Catarina" },
                    { (short)25, "Sudeste", "São Paulo" },
                    { (short)26, "Nordeste", "Sergipe" },
                    { (short)27, "Norte", "Tocantins" }
                });

            migrationBuilder.InsertData(
                schema: "contacts",
                table: "tb_area_code",
                columns: ["area_code_id", "area_code_region_id", "area_code_value"],
                values: new object[,]
                {
                    { (short)1, (short)25, "11" },
                    { (short)2, (short)25, "12" },
                    { (short)3, (short)25, "13" },
                    { (short)4, (short)25, "14" },
                    { (short)5, (short)25, "15" },
                    { (short)6, (short)25, "16" },
                    { (short)7, (short)25, "17" },
                    { (short)8, (short)25, "18" },
                    { (short)9, (short)25, "19" },
                    { (short)10, (short)19, "21" },
                    { (short)11, (short)19, "22" },
                    { (short)12, (short)19, "24" },
                    { (short)13, (short)8, "27" },
                    { (short)14, (short)8, "28" },
                    { (short)15, (short)13, "31" },
                    { (short)16, (short)13, "32" },
                    { (short)17, (short)13, "33" },
                    { (short)18, (short)13, "34" },
                    { (short)19, (short)13, "35" },
                    { (short)20, (short)13, "37" },
                    { (short)21, (short)13, "38" },
                    { (short)22, (short)16, "41" },
                    { (short)23, (short)16, "42" },
                    { (short)24, (short)16, "43" },
                    { (short)25, (short)16, "44" },
                    { (short)26, (short)16, "45" },
                    { (short)27, (short)16, "46" },
                    { (short)28, (short)24, "47" },
                    { (short)29, (short)24, "48" },
                    { (short)30, (short)24, "49" },
                    { (short)31, (short)21, "51" },
                    { (short)32, (short)21, "53" },
                    { (short)33, (short)21, "54" },
                    { (short)34, (short)21, "55" },
                    { (short)35, (short)7, "61" },
                    { (short)36, (short)9, "62" },
                    { (short)37, (short)27, "63" },
                    { (short)38, (short)9, "64" },
                    { (short)39, (short)11, "65" },
                    { (short)40, (short)11, "66" },
                    { (short)41, (short)12, "67" },
                    { (short)42, (short)1, "68" },
                    { (short)43, (short)22, "69" },
                    { (short)44, (short)5, "71" },
                    { (short)45, (short)5, "73" },
                    { (short)46, (short)5, "74" },
                    { (short)47, (short)5, "75" },
                    { (short)48, (short)5, "77" },
                    { (short)49, (short)26, "79" },
                    { (short)50, (short)17, "81" },
                    { (short)51, (short)2, "82" },
                    { (short)52, (short)15, "83" },
                    { (short)53, (short)20, "84" },
                    { (short)54, (short)6, "85" },
                    { (short)55, (short)18, "86" },
                    { (short)56, (short)17, "87" },
                    { (short)57, (short)6, "88" },
                    { (short)58, (short)18, "89" },
                    { (short)59, (short)14, "91" },
                    { (short)60, (short)4, "92" },
                    { (short)61, (short)14, "93" },
                    { (short)62, (short)14, "94" },
                    { (short)63, (short)23, "95" },
                    { (short)64, (short)3, "96" },
                    { (short)65, (short)4, "97" },
                    { (short)66, (short)10, "98" },
                    { (short)67, (short)10, "99" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_tb_area_code_area_code_region_id",
                schema: "contacts",
                table: "tb_area_code",
                column: "area_code_region_id");

            migrationBuilder.CreateIndex(
                name: "ix_tb_area_code_area_code_value",
                schema: "contacts",
                table: "tb_area_code",
                column: "area_code_value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tb_contact_contact_phone_id",
                schema: "contacts",
                table: "tb_contact",
                column: "contact_contact_phone_id");

            migrationBuilder.CreateIndex(
                name: "ix_tb_contact_phone_area_code_id",
                schema: "contacts",
                table: "tb_contact_phone",
                column: "contact_phone_area_code_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_contact",
                schema: "contacts");

            migrationBuilder.DropTable(
                name: "tb_contact_phone",
                schema: "contacts");

            migrationBuilder.DropTable(
                name: "tb_area_code",
                schema: "contacts");

            migrationBuilder.DropTable(
                name: "tb_region",
                schema: "contacts");
        }
    }
}