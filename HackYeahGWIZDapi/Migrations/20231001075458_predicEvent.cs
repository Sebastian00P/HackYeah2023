using Microsoft.EntityFrameworkCore.Migrations;

namespace HackYeahGWIZDapi.Migrations
{
    public partial class predicEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PredictionEvents",
                columns: table => new
                {
                    PredictionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocalizationId = table.Column<long>(type: "bigint", nullable: true),
                    PhotoId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredictionEvents", x => x.PredictionId);
                    table.ForeignKey(
                        name: "FK_PredictionEvents_EventPhotos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "EventPhotos",
                        principalColumn: "PhotoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PredictionEvents_Localizations_LocalizationId",
                        column: x => x.LocalizationId,
                        principalTable: "Localizations",
                        principalColumn: "LocalizationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PredictionEvents_LocalizationId",
                table: "PredictionEvents",
                column: "LocalizationId");

            migrationBuilder.CreateIndex(
                name: "IX_PredictionEvents_PhotoId",
                table: "PredictionEvents",
                column: "PhotoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PredictionEvents");
        }
    }
}
