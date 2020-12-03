using Microsoft.EntityFrameworkCore.Migrations;

namespace desafio.Migrations
{
    public partial class AddBolenaDelection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Produtos",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Fornecedores",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Clientes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Clientes");
        }
    }
}
