using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NBDGreenerGrass.Data.NBDMigrations
{
    /// <inheritdoc />
    public partial class RebuildingDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientRoles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Role = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRoles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InventoryDesc = table.Column<string>(type: "TEXT", nullable: true),
                    InventorySize = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    InventoryCode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    InventoryListPrice = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Labours",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LabourType = table.Column<string>(type: "TEXT", nullable: false),
                    LabourPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    LabourCost = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labours", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StaffRoles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Role = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffRoles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ContactFirst = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ContactLast = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Street = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Postal = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Province = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ClientRoleID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Clients_ClientRoles_ClientRoleID",
                        column: x => x.ClientRoleID,
                        principalTable: "ClientRoles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StaffFirst = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    StaffLast = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    StaffRoleID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Staffs_StaffRoles_StaffRoleID",
                        column: x => x.StaffRoleID,
                        principalTable: "StaffRoles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Start = table.Column<DateTime>(type: "TEXT", nullable: false),
                    End = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Street = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Province = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Postal = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Desc = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ClientID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Projects_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BidLabours",
                columns: table => new
                {
                    LabourID = table.Column<int>(type: "INTEGER", nullable: false),
                    BidID = table.Column<int>(type: "INTEGER", nullable: false),
                    HoursWorked = table.Column<int>(type: "INTEGER", nullable: false),
                    LabourType = table.Column<string>(type: "TEXT", nullable: false),
                    LabourPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    LabourCost = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidLabours", x => new { x.BidID, x.LabourID });
                    table.ForeignKey(
                        name: "FK_BidLabours_Labours_LabourID",
                        column: x => x.LabourID,
                        principalTable: "Labours",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BidMaterials",
                columns: table => new
                {
                    BidID = table.Column<int>(type: "INTEGER", nullable: false),
                    InventoryID = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    InventoryDesc = table.Column<string>(type: "TEXT", maxLength: 75, nullable: false),
                    InventorySize = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    InventoryCode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    InventoryListPrice = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidMaterials", x => new { x.BidID, x.InventoryID });
                    table.ForeignKey(
                        name: "FK_BidMaterials_Inventories_InventoryID",
                        column: x => x.InventoryID,
                        principalTable: "Inventories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Stage = table.Column<int>(type: "INTEGER", nullable: false),
                    DateMade = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1500, nullable: true),
                    DeniedClientReason = table.Column<string>(type: "TEXT", nullable: true),
                    DeniedManagerReason = table.Column<string>(type: "TEXT", nullable: true),
                    ProjectID = table.Column<int>(type: "INTEGER", nullable: false),
                    BidLabourBidID = table.Column<int>(type: "INTEGER", nullable: true),
                    BidLabourLabourID = table.Column<int>(type: "INTEGER", nullable: true),
                    BidMaterialBidID = table.Column<int>(type: "INTEGER", nullable: true),
                    BidMaterialInventoryID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Bids_BidLabours_BidLabourBidID_BidLabourLabourID",
                        columns: x => new { x.BidLabourBidID, x.BidLabourLabourID },
                        principalTable: "BidLabours",
                        principalColumns: new[] { "BidID", "LabourID" });
                    table.ForeignKey(
                        name: "FK_Bids_BidMaterials_BidMaterialBidID_BidMaterialInventoryID",
                        columns: x => new { x.BidMaterialBidID, x.BidMaterialInventoryID },
                        principalTable: "BidMaterials",
                        principalColumns: new[] { "BidID", "InventoryID" });
                    table.ForeignKey(
                        name: "FK_Bids_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BidLabours_LabourID",
                table: "BidLabours",
                column: "LabourID");

            migrationBuilder.CreateIndex(
                name: "IX_BidMaterials_InventoryID",
                table: "BidMaterials",
                column: "InventoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_BidLabourBidID_BidLabourLabourID",
                table: "Bids",
                columns: new[] { "BidLabourBidID", "BidLabourLabourID" });

            migrationBuilder.CreateIndex(
                name: "IX_Bids_BidMaterialBidID_BidMaterialInventoryID",
                table: "Bids",
                columns: new[] { "BidMaterialBidID", "BidMaterialInventoryID" });

            migrationBuilder.CreateIndex(
                name: "IX_Bids_ProjectID",
                table: "Bids",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ClientRoleID",
                table: "Clients",
                column: "ClientRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ClientID",
                table: "Projects",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_StaffRoleID",
                table: "Staffs",
                column: "StaffRoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_BidLabours_Bids_BidID",
                table: "BidLabours",
                column: "BidID",
                principalTable: "Bids",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BidMaterials_Bids_BidID",
                table: "BidMaterials",
                column: "BidID",
                principalTable: "Bids",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BidLabours_Bids_BidID",
                table: "BidLabours");

            migrationBuilder.DropForeignKey(
                name: "FK_BidMaterials_Bids_BidID",
                table: "BidMaterials");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "StaffRoles");

            migrationBuilder.DropTable(
                name: "Bids");

            migrationBuilder.DropTable(
                name: "BidLabours");

            migrationBuilder.DropTable(
                name: "BidMaterials");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Labours");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "ClientRoles");
        }
    }
}
