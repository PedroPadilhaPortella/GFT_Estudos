using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FuncionariosWA.Migrations
{
    public partial class DataAlocacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alocacao_Funcionarios_FuncionarioId",
                table: "Alocacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Alocacao_Vagas_VagaId",
                table: "Alocacao");

            migrationBuilder.DropIndex(
                name: "IX_Alocacao_FuncionarioId",
                table: "Alocacao");

            migrationBuilder.DropIndex(
                name: "IX_Alocacao_VagaId",
                table: "Alocacao");

            migrationBuilder.DropColumn(
                name: "FuncionarioId",
                table: "Alocacao");

            migrationBuilder.DropColumn(
                name: "VagaId",
                table: "Alocacao");

            migrationBuilder.AddColumn<DateTime>(
                name: "Data",
                table: "Alocacao",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "FuncionariosId",
                table: "Alocacao",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VagasId",
                table: "Alocacao",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alocacao_FuncionariosId",
                table: "Alocacao",
                column: "FuncionariosId");

            migrationBuilder.CreateIndex(
                name: "IX_Alocacao_VagasId",
                table: "Alocacao",
                column: "VagasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alocacao_Funcionarios_FuncionariosId",
                table: "Alocacao",
                column: "FuncionariosId",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Alocacao_Vagas_VagasId",
                table: "Alocacao",
                column: "VagasId",
                principalTable: "Vagas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alocacao_Funcionarios_FuncionariosId",
                table: "Alocacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Alocacao_Vagas_VagasId",
                table: "Alocacao");

            migrationBuilder.DropIndex(
                name: "IX_Alocacao_FuncionariosId",
                table: "Alocacao");

            migrationBuilder.DropIndex(
                name: "IX_Alocacao_VagasId",
                table: "Alocacao");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Alocacao");

            migrationBuilder.DropColumn(
                name: "FuncionariosId",
                table: "Alocacao");

            migrationBuilder.DropColumn(
                name: "VagasId",
                table: "Alocacao");

            migrationBuilder.AddColumn<int>(
                name: "FuncionarioId",
                table: "Alocacao",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VagaId",
                table: "Alocacao",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alocacao_FuncionarioId",
                table: "Alocacao",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Alocacao_VagaId",
                table: "Alocacao",
                column: "VagaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alocacao_Funcionarios_FuncionarioId",
                table: "Alocacao",
                column: "FuncionarioId",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Alocacao_Vagas_VagaId",
                table: "Alocacao",
                column: "VagaId",
                principalTable: "Vagas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
