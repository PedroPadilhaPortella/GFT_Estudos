using Microsoft.EntityFrameworkCore.Migrations;

namespace desafio.Migrations
{
    public partial class RemoverNivelAcessoDosClientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NivelAcesso",
                table: "Clientes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NivelAcesso",
                table: "Clientes",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
