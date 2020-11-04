using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FuncionariosWA.Migrations
{
    public partial class RelacaoVagaTecnologiaFuncionario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Matricula = table.Column<string>(nullable: true),
                    Cargo = table.Column<string>(nullable: true),
                    Inicio_wa = table.Column<DateTime>(nullable: false),
                    Termino_wa = table.Column<DateTime>(nullable: false),
                    AlocacaoId = table.Column<int>(nullable: false),
                    Local_de_TrabalhoId = table.Column<int>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funcionarios_GFT_Local_de_TrabalhoId",
                        column: x => x.Local_de_TrabalhoId,
                        principalTable: "GFT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vagas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Projeto = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    CodigoVaga = table.Column<string>(nullable: true),
                    AberturaVaga = table.Column<DateTime>(nullable: false),
                    QuantidadeVagas = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vagas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FuncionarioTecnologias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FuncionarioId = table.Column<int>(nullable: false),
                    TecnologiaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncionarioTecnologias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuncionarioTecnologias_Funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FuncionarioTecnologias_Tecnologias_TecnologiaId",
                        column: x => x.TecnologiaId,
                        principalTable: "Tecnologias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VagaTecnologias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VagaId = table.Column<int>(nullable: false),
                    TecnologiaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VagaTecnologias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VagaTecnologias_Tecnologias_TecnologiaId",
                        column: x => x.TecnologiaId,
                        principalTable: "Tecnologias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VagaTecnologias_Vagas_VagaId",
                        column: x => x.VagaId,
                        principalTable: "Vagas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_Local_de_TrabalhoId",
                table: "Funcionarios",
                column: "Local_de_TrabalhoId");

            migrationBuilder.CreateIndex(
                name: "IX_FuncionarioTecnologias_FuncionarioId",
                table: "FuncionarioTecnologias",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_FuncionarioTecnologias_TecnologiaId",
                table: "FuncionarioTecnologias",
                column: "TecnologiaId");

            migrationBuilder.CreateIndex(
                name: "IX_VagaTecnologias_TecnologiaId",
                table: "VagaTecnologias",
                column: "TecnologiaId");

            migrationBuilder.CreateIndex(
                name: "IX_VagaTecnologias_VagaId",
                table: "VagaTecnologias",
                column: "VagaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuncionarioTecnologias");

            migrationBuilder.DropTable(
                name: "VagaTecnologias");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "Vagas");
        }
    }
}
