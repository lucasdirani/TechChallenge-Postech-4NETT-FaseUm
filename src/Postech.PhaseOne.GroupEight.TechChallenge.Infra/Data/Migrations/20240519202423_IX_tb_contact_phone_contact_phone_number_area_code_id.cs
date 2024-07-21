using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace Postech.PhaseOne.GroupEight.TechChallenge.Infra.Data.Migrations
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public partial class IX_tb_contact_phone_contact_phone_number_area_code_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_tb_contact_phone_contact_phone_number",
                schema: "contacts",
                table: "tb_contact_phone");

            migrationBuilder.RenameIndex(
                name: "ix_tb_contact_phone_area_code_id",
                schema: "contacts",
                table: "tb_contact_phone",
                newName: "IX_tb_contact_phone_contact_phone_area_code_id");

            migrationBuilder.CreateIndex(
                name: "ix_tb_contact_phone_contact_phone_number_area_code_id",
                schema: "contacts",
                table: "tb_contact_phone",
                columns: ["contact_phone_number", "contact_phone_area_code_id"],
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_tb_contact_phone_contact_phone_number_area_code_id",
                schema: "contacts",
                table: "tb_contact_phone");

            migrationBuilder.RenameIndex(
                name: "IX_tb_contact_phone_contact_phone_area_code_id",
                schema: "contacts",
                table: "tb_contact_phone",
                newName: "ix_tb_contact_phone_area_code_id");

            migrationBuilder.CreateIndex(
                name: "ix_tb_contact_phone_contact_phone_number",
                schema: "contacts",
                table: "tb_contact_phone",
                column: "contact_phone_number",
                unique: true);
        }
    }
}