using Microsoft.EntityFrameworkCore.Migrations;

namespace FuncionariosWA.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TecnologiaId",
                table: "Funcionarios",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_TecnologiaId",
                table: "Funcionarios",
                column: "TecnologiaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionarios_Tecnologias_TecnologiaId",
                table: "Funcionarios",
                column: "TecnologiaId",
                principalTable: "Tecnologias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcionarios_Tecnologias_TecnologiaId",
                table: "Funcionarios");

            migrationBuilder.DropIndex(
                name: "IX_Funcionarios_TecnologiaId",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "TecnologiaId",
                table: "Funcionarios");
        }
    }
}
