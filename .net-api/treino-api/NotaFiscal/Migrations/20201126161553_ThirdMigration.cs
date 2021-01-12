using Microsoft.EntityFrameworkCore.Migrations;

namespace NotaFiscal.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PrecoUnitario",
                table: "Produtos",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "Valor",
                table: "NotasFiscais",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "PrecoUnitario",
                table: "Produtos",
                type: "float",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "Valor",
                table: "NotasFiscais",
                type: "float",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
