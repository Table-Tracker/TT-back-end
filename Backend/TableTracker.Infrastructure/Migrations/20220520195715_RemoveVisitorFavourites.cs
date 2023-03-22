using Microsoft.EntityFrameworkCore.Migrations;

namespace TableTracker.Infrastructure.Migrations
{
    public partial class RemoveVisitorFavourites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitorFavourites");

            migrationBuilder.AddColumn<long>(
                name: "VisitorId",
                table: "Restaurants",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_VisitorId",
                table: "Restaurants",
                column: "VisitorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_User_VisitorId",
                table: "Restaurants",
                column: "VisitorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_User_VisitorId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_VisitorId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "VisitorId",
                table: "Restaurants");

            migrationBuilder.CreateTable(
                name: "VisitorFavourites",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantId = table.Column<long>(type: "bigint", nullable: false),
                    VisitorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitorFavourites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitorFavourites_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisitorFavourites_User_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitorFavourites_RestaurantId",
                table: "VisitorFavourites",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitorFavourites_VisitorId",
                table: "VisitorFavourites",
                column: "VisitorId");
        }
    }
}
