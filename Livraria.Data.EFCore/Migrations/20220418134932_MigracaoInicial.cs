using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LivrariaAPI.Migrations
{
    public partial class MigracaoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autores",
                columns: table => new
                {
                    aut_codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    aut_nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    aut_dtnascimento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autores", x => x.aut_codigo);
                });

            migrationBuilder.CreateTable(
                name: "Livros",
                columns: table => new
                {
                    liv_codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    liv_titulo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    liv_datacadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    liv_preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livros", x => x.liv_codigo);
                });

            migrationBuilder.CreateTable(
                name: "Livros_autores",
                columns: table => new
                {
                    liv_codigo = table.Column<int>(type: "int", nullable: false),
                    aut_codigo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livros_autores", x => new { x.liv_codigo, x.aut_codigo });
                    table.ForeignKey(
                        name: "FK_Livros_autores_Autores_aut_codigo",
                        column: x => x.aut_codigo,
                        principalTable: "Autores",
                        principalColumn: "aut_codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Livros_autores_Livros_liv_codigo",
                        column: x => x.liv_codigo,
                        principalTable: "Livros",
                        principalColumn: "liv_codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Livros_autores_aut_codigo",
                table: "Livros_autores",
                column: "aut_codigo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Livros_autores");

            migrationBuilder.DropTable(
                name: "Autores");

            migrationBuilder.DropTable(
                name: "Livros");
        }
    }
}
