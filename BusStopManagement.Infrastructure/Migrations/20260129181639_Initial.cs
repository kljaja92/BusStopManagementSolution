using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusStopManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusStop",
                columns: table => new
                {
                    BusStopID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusStop", x => x.BusStopID);
                });

            migrationBuilder.CreateTable(
                name: "Departure",
                columns: table => new
                {
                    DepartureID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAndTimeOfDeparture = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfSeats = table.Column<byte>(type: "tinyint", nullable: false),
                    BusStopID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departure", x => x.DepartureID);
                    table.ForeignKey(
                        name: "FK_Departure_BusStop_BusStopID",
                        column: x => x.BusStopID,
                        principalTable: "BusStop",
                        principalColumn: "BusStopID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departure_BusStopID",
                table: "Departure",
                column: "BusStopID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departure");

            migrationBuilder.DropTable(
                name: "BusStop");
        }
    }
}
