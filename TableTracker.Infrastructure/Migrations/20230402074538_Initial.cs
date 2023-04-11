using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TableTracker.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cuisines",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CuisineName = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuisines", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Franchises",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Franchises", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CuisineRestaurant",
                columns: table => new
                {
                    CuisinesId = table.Column<long>(type: "bigint", nullable: false),
                    RestaurantsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuisineRestaurant", x => new { x.CuisinesId, x.RestaurantsId });
                    table.ForeignKey(
                        name: "FK_CuisineRestaurant_Cuisines_CuisinesId",
                        column: x => x.CuisinesId,
                        principalTable: "Cuisines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Rating = table.Column<float>(type: "float", nullable: false),
                    PriceRange = table.Column<int>(type: "int", nullable: false),
                    NumberOfTables = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    DateOfOpening = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Menu = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Website = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MainImageId = table.Column<long>(type: "bigint", nullable: true),
                    FranchiseId = table.Column<long>(type: "bigint", nullable: false),
                    LayoutId = table.Column<long>(type: "bigint", nullable: true),
                    ManagerId = table.Column<long>(type: "bigint", nullable: true),
                    VisitorId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Restaurants_Franchises_FranchiseId",
                        column: x => x.FranchiseId,
                        principalTable: "Franchises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RestaurantId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Layouts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LayoutData = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    RestaurantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Layouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Layouts_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    NumberOfSeats = table.Column<int>(type: "int", nullable: false),
                    Floor = table.Column<int>(type: "int", nullable: false),
                    TableSize = table.Column<double>(type: "double", nullable: false),
                    RestaurantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tables_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Location = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateOfBirth = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AvatarId = table.Column<long>(type: "bigint", nullable: true),
                    Discriminator = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ManagerState = table.Column<int>(type: "int", nullable: true),
                    RestaurantId = table.Column<long>(type: "bigint", nullable: true),
                    GeneralTrustFactor = table.Column<float>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Image_AvatarId",
                        column: x => x.AvatarId,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    VisitorId = table.Column<long>(type: "bigint", nullable: false),
                    TableId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_User_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RestaurantVisitors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RestaurantRate = table.Column<float>(type: "float", nullable: false),
                    TimesVisited = table.Column<int>(type: "int", nullable: false),
                    AverageMoneySpent = table.Column<double>(type: "double", nullable: false),
                    RestaurantId = table.Column<long>(type: "bigint", nullable: false),
                    VisitorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantVisitors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestaurantVisitors_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestaurantVisitors_User_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VisitorHistorys",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    VisitorId = table.Column<long>(type: "bigint", nullable: false),
                    RestaurantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitorHistorys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitorHistorys_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisitorHistorys_User_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CuisineRestaurant_RestaurantsId",
                table: "CuisineRestaurant",
                column: "RestaurantsId");

            migrationBuilder.CreateIndex(
                name: "IX_Cuisines_CuisineName",
                table: "Cuisines",
                column: "CuisineName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Franchises_Name",
                table: "Franchises",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Image_RestaurantId",
                table: "Image",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Layouts_RestaurantId",
                table: "Layouts",
                column: "RestaurantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TableId",
                table: "Reservations",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_VisitorId",
                table: "Reservations",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_FranchiseId",
                table: "Restaurants",
                column: "FranchiseId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_MainImageId",
                table: "Restaurants",
                column: "MainImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_VisitorId",
                table: "Restaurants",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantVisitors_RestaurantId",
                table: "RestaurantVisitors",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantVisitors_VisitorId",
                table: "RestaurantVisitors",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_RestaurantId",
                table: "Tables",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_User_AvatarId",
                table: "User",
                column: "AvatarId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_RestaurantId",
                table: "User",
                column: "RestaurantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VisitorHistorys_RestaurantId",
                table: "VisitorHistorys",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitorHistorys_VisitorId",
                table: "VisitorHistorys",
                column: "VisitorId");

            migrationBuilder.AddForeignKey(
                name: "FK_CuisineRestaurant_Restaurants_RestaurantsId",
                table: "CuisineRestaurant",
                column: "RestaurantsId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Image_MainImageId",
                table: "Restaurants",
                column: "MainImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Image_Restaurants_RestaurantId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Restaurants_RestaurantId",
                table: "User");

            migrationBuilder.DropTable(
                name: "CuisineRestaurant");

            migrationBuilder.DropTable(
                name: "Layouts");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "RestaurantVisitors");

            migrationBuilder.DropTable(
                name: "VisitorHistorys");

            migrationBuilder.DropTable(
                name: "Cuisines");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "Restaurants");

            migrationBuilder.DropTable(
                name: "Franchises");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Image");
        }
    }
}
