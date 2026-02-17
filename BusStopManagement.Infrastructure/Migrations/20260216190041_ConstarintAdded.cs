using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusStopManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConstarintAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BusStop_BusStopName",
                table: "BusStop",
                column: "BusStopName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BusStop_BusStopName",
                table: "BusStop");
        }
    }
}
