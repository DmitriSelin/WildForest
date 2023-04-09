using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WildForest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class WeatherMarksMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherMarks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    MediumMark = table.Column<double>(type: "double precision", nullable: false),
                    WeatherId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherMarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherMarks_WeatherForecasts_WeatherId",
                        column: x => x.WeatherId,
                        principalTable: "WeatherForecasts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherMarks_WeatherId",
                table: "WeatherMarks",
                column: "WeatherId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherMarks");
        }
    }
}
