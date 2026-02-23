using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Flight.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlightNumber = table.Column<string>(type: "TEXT", nullable: false),
                    FromCityId = table.Column<string>(type: "TEXT", nullable: false),
                    ToCityId = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightId);
                    table.ForeignKey(
                        name: "FK_Flights_Cities_FromCityId",
                        column: x => x.FromCityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Flights_Cities_ToCityId",
                        column: x => x.ToCityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "Name" },
                values: new object[,]
                {
                    { "CHI", "Chicago" },
                    { "DXB", "Dubai" },
                    { "HKG", "Hong Kong" },
                    { "LON", "London" },
                    { "NYC", "New York" },
                    { "SFO", "San Francisco" }
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Date", "FlightNumber", "FromCityId", "Price", "ToCityId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "UA3321", "CHI", 235m, "NYC" },
                    { 2, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "QA1078", "DXB", 590m, "LON" },
                    { 3, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "CA9087", "HKG", 900m, "SFO" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_FromCityId",
                table: "Flights",
                column: "FromCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_ToCityId",
                table: "Flights",
                column: "ToCityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
