using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Chronos.API.Dados.Migrations
{
    public partial class MigrationInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contratos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ValorDaHora = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Folhas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ContratoId = table.Column<Guid>(nullable: false),
                    DataFinal = table.Column<DateTime>(nullable: false),
                    DataInicial = table.Column<DateTime>(nullable: false),
                    QuantidadePrevistaDeDiasÚteis = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folhas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Folhas_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_Folhas_ContratoId",
                table: "Folhas",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_Períodos_FolhaId",
                table: "Períodos",
                column: "FolhaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Períodos");

            migrationBuilder.DropTable(
                name: "Folhas");

            migrationBuilder.DropTable(
                name: "Contratos");
        }
    }
}
