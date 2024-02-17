using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RinhaBackend.Api.Migrations
{
    /// <inheritdoc />
    public partial class _02initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Limit",
                table: "Customers",
                newName: "MaxLimit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxLimit",
                table: "Customers",
                newName: "Limit");
        }
    }
}
