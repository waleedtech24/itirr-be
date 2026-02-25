using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITIRR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AgencyName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgencyName",
                table: "AspNetUsers");
        }
    }
}
