using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorBiblioteca.API.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseEntityFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
        name: "CreatedAt",
        table: "Livros",
        type: "datetime2",
        nullable: false,
        defaultValue: DateTime.Now);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Livros",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.Now);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Emprestimos",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.Now);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Emprestimos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "CreatedAt", table: "Livros");
            migrationBuilder.DropColumn(name: "IsDeleted", table: "Livros");

            migrationBuilder.DropColumn(name: "CreatedAt", table: "Usuarios");
            migrationBuilder.DropColumn(name: "IsDeleted", table: "Usuarios");

            migrationBuilder.DropColumn(name: "CreatedAt", table: "Emprestimos");
            migrationBuilder.DropColumn(name: "IsDeleted", table: "Emprestimos");

        }
    }
}
