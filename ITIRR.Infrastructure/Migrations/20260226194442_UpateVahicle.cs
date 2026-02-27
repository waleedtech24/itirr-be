using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITIRR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpateVahicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PHVLicenceExpiryDate",
                table: "VehicleListings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PHVLicenceNumber",
                table: "VehicleListings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlateNumber",
                table: "VehicleListings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VehicleColor",
                table: "VehicleListings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VehicleMakeModel",
                table: "VehicleListings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YearOfManufacture",
                table: "VehicleListings",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PHVLicenceExpiryDate",
                table: "VehicleListings");

            migrationBuilder.DropColumn(
                name: "PHVLicenceNumber",
                table: "VehicleListings");

            migrationBuilder.DropColumn(
                name: "PlateNumber",
                table: "VehicleListings");

            migrationBuilder.DropColumn(
                name: "VehicleColor",
                table: "VehicleListings");

            migrationBuilder.DropColumn(
                name: "VehicleMakeModel",
                table: "VehicleListings");

            migrationBuilder.DropColumn(
                name: "YearOfManufacture",
                table: "VehicleListings");
        }
    }
}
