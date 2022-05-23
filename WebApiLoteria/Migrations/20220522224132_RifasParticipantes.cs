using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiLoteria.Migrations
{
    public partial class RifasParticipantes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RifasParticipantes",
                columns: table => new
                {
                    RifaId = table.Column<int>(type: "int", nullable: false),
                    ParticipanteId = table.Column<int>(type: "int", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RifasParticipantes", x => new { x.RifaId, x.ParticipanteId });
                    table.ForeignKey(
                        name: "FK_RifasParticipantes_Participantes_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "Participantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RifasParticipantes_Rifas_RifaId",
                        column: x => x.RifaId,
                        principalTable: "Rifas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RifasParticipantes_ParticipanteId",
                table: "RifasParticipantes",
                column: "ParticipanteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RifasParticipantes");
        }
    }
}
