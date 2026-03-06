using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITIRR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateListeningTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "JetListings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "BoatListings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_JetListings_OwnerId",
                table: "JetListings",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_BoatListings_OwnerId",
                table: "BoatListings",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoatListings_AspNetUsers_OwnerId",
                table: "BoatListings",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JetListings_AspNetUsers_OwnerId",
                table: "JetListings",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoatListings_AspNetUsers_OwnerId",
                table: "BoatListings");

            migrationBuilder.DropForeignKey(
                name: "FK_JetListings_AspNetUsers_OwnerId",
                table: "JetListings");

            migrationBuilder.DropIndex(
                name: "IX_JetListings_OwnerId",
                table: "JetListings");

            migrationBuilder.DropIndex(
                name: "IX_BoatListings_OwnerId",
                table: "BoatListings");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "JetListings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "BoatListings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
