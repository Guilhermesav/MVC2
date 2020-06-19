using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC2AT.Data.Migrations
{
    public partial class RecreateEstadoDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Capital = table.Column<string>(nullable: true),
                    Sigla = table.Column<string>(nullable: true),
                    População = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cidades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    NomeEstado = table.Column<string>(nullable: true),
                    População = table.Column<int>(nullable: false),
                    EstadoEntityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cidades_Estados_EstadoEntityId",
                        column: x => x.EstadoEntityId,
                        principalTable: "Estados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cidades_EstadoEntityId",
                table: "Cidades",
                column: "EstadoEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Estados_Capital",
                table: "Estados",
                column: "Capital",
                unique: true,
                filter: "[Capital] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cidades");

            migrationBuilder.DropTable(
                name: "Estados");
        }
    }
}
