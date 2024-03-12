using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NBDGreenerGrass.Data.NBDMigrations
{
    /// <inheritdoc />
    public partial class BidUpdateEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BidStaffID",
                table: "Bids");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BidStaffID",
                table: "Bids",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
