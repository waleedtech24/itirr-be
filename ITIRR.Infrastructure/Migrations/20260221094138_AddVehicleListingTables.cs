using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITIRR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVehicleListingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DriversLicences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriversLicences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriversLicences_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleListings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VehicleType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentStep = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsOlderThan1981 = table.Column<bool>(type: "bit", nullable: false),
                    LicencePlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OdometerReading = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transmission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Features = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxCertified = table.Column<bool>(type: "bit", nullable: true),
                    HasSalvageTitle = table.Column<bool>(type: "bit", nullable: true),
                    PrimaryGoal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsageFrequency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShareFrequency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdvanceNotice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinTripDuration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxTripDuration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinTwoDayWeekend = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleListings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleListings_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleListings_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VehicleListings_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PCOLicences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VehicleListingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VehicleMakeModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearOfManufacture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PHVLicenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PHVLicenceExpiryDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoadTaxCertificateUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MOTCertificateUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoadTaxCertificate2Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleLogbookUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PCOLicences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PCOLicences_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PCOLicences_VehicleListings_VehicleListingId",
                        column: x => x.VehicleListingId,
                        principalTable: "VehicleListings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VehicleListingMedia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleListingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MediaType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleListingMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleListingMedia_VehicleListings_VehicleListingId",
                        column: x => x.VehicleListingId,
                        principalTable: "VehicleListings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriversLicences_UserId",
                table: "DriversLicences",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PCOLicences_UserId",
                table: "PCOLicences",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PCOLicences_VehicleListingId",
                table: "PCOLicences",
                column: "VehicleListingId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleListingMedia_VehicleListingId",
                table: "VehicleListingMedia",
                column: "VehicleListingId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleListings_CityId",
                table: "VehicleListings",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleListings_CountryId",
                table: "VehicleListings",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleListings_OwnerId",
                table: "VehicleListings",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriversLicences");

            migrationBuilder.DropTable(
                name: "PCOLicences");

            migrationBuilder.DropTable(
                name: "VehicleListingMedia");

            migrationBuilder.DropTable(
                name: "VehicleListings");
        }
    }
}
