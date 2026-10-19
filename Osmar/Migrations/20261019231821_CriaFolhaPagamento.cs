using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Osmar.Migrations
{
    /// <inheritdoc />
    public partial class CriaFolhaPagamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FolhaPagamento",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Cpf = table.Column<string>(type: "TEXT", nullable: false),
                    Mes = table.Column<int>(type: "INTEGER", nullable: false),
                    Ano = table.Column<int>(type: "INTEGER", nullable: false),
                    HorasTrabalhadas = table.Column<double>(type: "REAL", nullable: false),
                    ValorHora = table.Column<double>(type: "REAL", nullable: false),
                    SalarioBruto = table.Column<double>(type: "REAL", nullable: false),
                    ImpostoRenda = table.Column<double>(type: "REAL", nullable: false),
                    Inss = table.Column<double>(type: "REAL", nullable: false),
                    Fgts = table.Column<double>(type: "REAL", nullable: false),
                    SalarioLiquido = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolhaPagamento", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FolhaPagamento");
        }
    }
}
