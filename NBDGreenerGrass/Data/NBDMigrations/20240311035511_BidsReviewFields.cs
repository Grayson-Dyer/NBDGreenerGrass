using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NBDGreenerGrass.Data.NBDMigrations
{
    /// <inheritdoc />
    public partial class BidsReviewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeniedClientReason",
                table: "Bids",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeniedManagerReason",
                table: "Bids",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeniedClientReason",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "DeniedManagerReason",
                table: "Bids");
        }
    }
}
