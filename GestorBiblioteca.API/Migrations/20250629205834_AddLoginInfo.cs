using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorBiblioteca.API.Migrations
{
    /// <inheritdoc />
    public partial class AddLoginInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Usuarios");
        }
    }
}
