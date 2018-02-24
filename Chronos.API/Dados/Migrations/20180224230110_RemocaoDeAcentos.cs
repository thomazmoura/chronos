using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Chronos.API.Dados.Migrations
{
    public partial class RemocaoDeAcentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Períodos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataFinal",
                table: "Folhas",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.CreateTable(
                name: "Periodos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    FolhaId = table.Column<Guid>(nullable: false),
                    HorarioDeEncerramento = table.Column<DateTime>(nullable: false),
                    HorarioDeInicio = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periodos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Periodos_Folhas_FolhaId",
                        column: x => x.FolhaId,
                        principalTable: "Folhas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Periodos_FolhaId",
                table: "Periodos",
                column: "FolhaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Periodos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataFinal",
                table: "Folhas",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Períodos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Descrição = table.Column<string>(nullable: true),
                    FolhaId = table.Column<Guid>(nullable: false),
                    HorárioDeEncerramento = table.Column<DateTime>(nullable: false),
                    HorárioDeInício = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Períodos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Períodos_Folhas_FolhaId",
                        column: x => x.FolhaId,
                        principalTable: "Folhas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Períodos_FolhaId",
                table: "Períodos",
                column: "FolhaId");
        }
    }
}
