using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITIRR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddYetTbls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoatListings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoatType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoatMake = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoatModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoatYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoatLength = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassengerCapacity = table.Column<int>(type: "int", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HullMaterial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FuelType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryGoal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsageFrequency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShareFrequency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdvanceNotice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinTripDuration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxTripDuration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinTwoDayWeekend = table.Column<bool>(type: "bit", nullable: false),
                    SkipperFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkipperMiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SkipperLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkipperLicenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkipperLicenceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkipperLicenceExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SkipperLicenceDocUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentStep = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoatListings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JetListings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HangarLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeAirport = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AircraftMake = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AircraftModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AircraftYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TailNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AircraftCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassengerCapacity = table.Column<int>(type: "int", nullable: false),
                    RangeNauticalMiles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CruisingSpeed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EngineType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CabinFeatures = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirworthinessDocUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsuranceDocUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDocUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastMaintenanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SafetyCertifications = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PilotFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PilotLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PilotLicenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PilotLicenceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PilotLicenceExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CrewCount = table.Column<int>(type: "int", nullable: false),
                    AdvanceNotice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinTripDuration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxTripDuration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CancellationPolicy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryGoal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsageFrequency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShareFrequency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentStep = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JetListings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoatListingMedia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BoatListingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_BoatListingMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoatListingMedia_BoatListings_BoatListingId",
                        column: x => x.BoatListingId,
                        principalTable: "BoatListings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JetListingMedia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JetListingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_JetListingMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JetListingMedia_JetListings_JetListingId",
                        column: x => x.JetListingId,
                        principalTable: "JetListings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoatListingMedia_BoatListingId",
                table: "BoatListingMedia",
                column: "BoatListingId");

            migrationBuilder.CreateIndex(
                name: "IX_JetListingMedia_JetListingId",
                table: "JetListingMedia",
                column: "JetListingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoatListingMedia");

            migrationBuilder.DropTable(
                name: "JetListingMedia");

            migrationBuilder.DropTable(
                name: "BoatListings");

            migrationBuilder.DropTable(
                name: "JetListings");
        }
    }
}
