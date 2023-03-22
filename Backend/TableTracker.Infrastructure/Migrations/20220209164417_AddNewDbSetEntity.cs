using Microsoft.EntityFrameworkCore.Migrations;

namespace TableTracker.Infrastructure.Migrations
{
    public partial class AddNewDbSetEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitorFavouritess_Restaurants_RestaurantId",
                table: "VisitorFavouritess");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitorFavouritess_Visitors_VisitorId",
                table: "VisitorFavouritess");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VisitorFavouritess",
                table: "VisitorFavouritess");

            migrationBuilder.RenameTable(
                name: "VisitorFavouritess",
                newName: "VisitorFavourites");

            migrationBuilder.RenameIndex(
                name: "IX_VisitorFavouritess_VisitorId",
                table: "VisitorFavourites",
                newName: "IX_VisitorFavourites_VisitorId");

            migrationBuilder.RenameIndex(
                name: "IX_VisitorFavouritess_RestaurantId",
                table: "VisitorFavourites",
                newName: "IX_VisitorFavourites_RestaurantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VisitorFavourites",
                table: "VisitorFavourites",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitorFavourites_Restaurants_RestaurantId",
                table: "VisitorFavourites",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitorFavourites_Visitors_VisitorId",
                table: "VisitorFavourites",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitorFavourites_Restaurants_RestaurantId",
                table: "VisitorFavourites");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitorFavourites_Visitors_VisitorId",
                table: "VisitorFavourites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VisitorFavourites",
                table: "VisitorFavourites");

            migrationBuilder.RenameTable(
                name: "VisitorFavourites",
                newName: "VisitorFavouritess");

            migrationBuilder.RenameIndex(
                name: "IX_VisitorFavourites_VisitorId",
                table: "VisitorFavouritess",
                newName: "IX_VisitorFavouritess_VisitorId");

            migrationBuilder.RenameIndex(
                name: "IX_VisitorFavourites_RestaurantId",
                table: "VisitorFavouritess",
                newName: "IX_VisitorFavouritess_RestaurantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VisitorFavouritess",
                table: "VisitorFavouritess",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitorFavouritess_Restaurants_RestaurantId",
                table: "VisitorFavouritess",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitorFavouritess_Visitors_VisitorId",
                table: "VisitorFavouritess",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
