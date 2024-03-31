using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NBDGreenerGrass.Data.NBDMigrations
{
    /// <inheritdoc />
    public partial class AddedApprovedReasonToBids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovedClientReason",
                table: "Bids",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedManagerReason",
                table: "Bids",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedClientReason",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "ApprovedManagerReason",
                table: "Bids");
        }
    }
}
