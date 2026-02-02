using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusStopManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedBusStopEntityAndAddedSchemaConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Destination",
                table: "Departure",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "BusStopAddress",
                table: "BusStop",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BusStopName",
                table: "BusStop",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusStopAddress",
                table: "BusStop");

            migrationBuilder.DropColumn(
                name: "BusStopName",
                table: "BusStop");

            migrationBuilder.AlterColumn<string>(
                name: "Destination",
                table: "Departure",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
