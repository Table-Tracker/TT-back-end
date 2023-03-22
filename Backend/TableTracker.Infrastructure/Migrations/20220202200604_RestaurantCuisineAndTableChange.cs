using Microsoft.EntityFrameworkCore.Migrations;

namespace TableTracker.Infrastructure.Migrations
{
    public partial class RestaurantCuisineAndTableChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Waiters_WaiterId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_WaiterId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "WaiterId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "Cuisine",
                table: "Restaurants");

            migrationBuilder.CreateTable(
                name: "Cuisines",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cuisine = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuisines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CuisinesRestaurant",
                columns: table => new
                {
                    CuisinesId = table.Column<long>(type: "bigint", nullable: false),
                    RestaurantsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuisinesRestaurant", x => new { x.CuisinesId, x.RestaurantsId });
                    table.ForeignKey(
                        name: "FK_CuisinesRestaurant_Cuisines_CuisinesId",
                        column: x => x.CuisinesId,
                        principalTable: "Cuisines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CuisinesRestaurant_Restaurants_RestaurantsId",
                        column: x => x.RestaurantsId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tables_ServingWaiterId",
                table: "Tables",
                column: "ServingWaiterId");

            migrationBuilder.CreateIndex(
                name: "IX_CuisinesRestaurant_RestaurantsId",
                table: "CuisinesRestaurant",
                column: "RestaurantsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Waiters_ServingWaiterId",
                table: "Tables",
                column: "ServingWaiterId",
                principalTable: "Waiters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Waiters_ServingWaiterId",
                table: "Tables");

            migrationBuilder.DropTable(
                name: "CuisinesRestaurant");

            migrationBuilder.DropTable(
                name: "Cuisines");

            migrationBuilder.DropIndex(
                name: "IX_Tables_ServingWaiterId",
                table: "Tables");

            migrationBuilder.AddColumn<long>(
                name: "WaiterId",
                table: "Tables",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Cuisine",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tables_WaiterId",
                table: "Tables",
                column: "WaiterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Waiters_WaiterId",
                table: "Tables",
                column: "WaiterId",
                principalTable: "Waiters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
