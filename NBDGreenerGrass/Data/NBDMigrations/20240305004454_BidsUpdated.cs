using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NBDGreenerGrass.Data.NBDMigrations
{
    /// <inheritdoc />
    public partial class BidsUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateMade",
                table: "Bids",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Bids",
                type: "TEXT",
                maxLength: 1500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateMade",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Bids");
        }
    }
}
