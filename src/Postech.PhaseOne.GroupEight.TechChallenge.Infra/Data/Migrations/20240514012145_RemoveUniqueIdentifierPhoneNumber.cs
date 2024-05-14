using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Postech.PhaseOne.GroupEight.TechChallenge.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueIdentifierPhoneNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_tb_contact_phone_contact_phone_number",
                schema: "contacts",
                table: "tb_contact_phone");

            migrationBuilder.CreateIndex(
                name: "ix_tb_contact_phone_contact_phone_number",
                schema: "contacts",
                table: "tb_contact_phone",
                column: "contact_phone_number");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_tb_contact_phone_contact_phone_number",
                schema: "contacts",
                table: "tb_contact_phone");

            migrationBuilder.CreateIndex(
                name: "ix_tb_contact_phone_contact_phone_number",
                schema: "contacts",
                table: "tb_contact_phone",
                column: "contact_phone_number",
                unique: true);
        }
    }
}
