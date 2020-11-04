using Microsoft.EntityFrameworkCore.Migrations;

namespace FuncionariosWA.Migrations
{
    public partial class PrimaryMigrateTec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Tecnologias",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Nome",
                table: "Tecnologias",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
