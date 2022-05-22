using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiLoteria.Migrations
{
    public partial class FechaInscripcionParticipante : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaInscripcion",
                table: "Participantes",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaInscripcion",
                table: "Participantes");
        }
    }
}
