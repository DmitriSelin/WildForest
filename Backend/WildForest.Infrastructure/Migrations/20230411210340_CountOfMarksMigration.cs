using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WildForest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CountOfMarksMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "MediumMark",
                table: "WeatherMarks",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<long>(
                name: "CountOfMarks",
                table: "WeatherMarks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountOfMarks",
                table: "WeatherMarks");

            migrationBuilder.AlterColumn<double>(
                name: "MediumMark",
                table: "WeatherMarks",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldDefaultValue: 0.0);
        }
    }
}
