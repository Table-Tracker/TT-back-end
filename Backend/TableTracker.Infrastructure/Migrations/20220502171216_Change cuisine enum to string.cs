using Microsoft.EntityFrameworkCore.Migrations;

namespace TableTracker.Infrastructure.Migrations
{
    public partial class Changecuisineenumtostring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cuisines_CuisineEnum",
                table: "Cuisines");

            migrationBuilder.DropColumn(
                name: "CuisineEnum",
                table: "Cuisines");

            migrationBuilder.AddColumn<string>(
                name: "CuisineName",
                table: "Cuisines",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Cuisines_CuisineName",
                table: "Cuisines",
                column: "CuisineName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cuisines_CuisineName",
                table: "Cuisines");

            migrationBuilder.DropColumn(
                name: "CuisineName",
                table: "Cuisines");

            migrationBuilder.AddColumn<int>(
                name: "CuisineEnum",
                table: "Cuisines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cuisines_CuisineEnum",
                table: "Cuisines",
                column: "CuisineEnum",
                unique: true);
        }
    }
}
