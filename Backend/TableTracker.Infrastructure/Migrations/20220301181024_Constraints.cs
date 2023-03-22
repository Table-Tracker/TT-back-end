using Microsoft.EntityFrameworkCore.Migrations;

namespace TableTracker.Infrastructure.Migrations
{
    public partial class Constraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Franchises",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Franchises_Name",
                table: "Franchises",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cuisines_CuisineEnum",
                table: "Cuisines",
                column: "CuisineEnum",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Franchises_Name",
                table: "Franchises");

            migrationBuilder.DropIndex(
                name: "IX_Cuisines_CuisineEnum",
                table: "Cuisines");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Franchises",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
