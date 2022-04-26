using Microsoft.EntityFrameworkCore.Migrations;

namespace LivrariaAPI.Migrations
{
    public partial class IncluidoIndicePorCPFnaTabelaAutor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "aut_cpf",
                table: "Autores",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "Indice_Autores",
                table: "Autores",
                column: "aut_cpf",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Indice_Autores",
                table: "Autores");

            migrationBuilder.AlterColumn<string>(
                name: "aut_cpf",
                table: "Autores",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);
        }
    }
}
