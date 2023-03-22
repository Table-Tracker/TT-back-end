using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TableTracker.Infrastructure.Migrations
{
    public partial class AddImagesAndReworkRestaurants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Layouts_Restaurants_RestaurantId",
                table: "Layouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Waiters_WaiterId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationVisitor_Visitors_VisitorsId",
                table: "ReservationVisitor");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Franchises_FranchiseId",
                table: "Restaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantVisitors_Visitors_VisitorId",
                table: "RestaurantVisitors");

            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Waiters_WaiterId",
                table: "Tables");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitorFavourites_Visitors_VisitorId",
                table: "VisitorFavourites");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitorHistorys_Visitors_VisitorId",
                table: "VisitorHistorys");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "Waiters");

            migrationBuilder.DropIndex(
                name: "IX_Tables_WaiterId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_WaiterId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Visitors",
                table: "Visitors");

            migrationBuilder.DropColumn(
                name: "CoordX",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "CoordY",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "WaiterId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "CoordX",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "CoordY",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "WaiterId",
                table: "Reservations");

            migrationBuilder.RenameTable(
                name: "Visitors",
                newName: "User");

            migrationBuilder.RenameColumn(
                name: "Avatar",
                table: "User",
                newName: "Location");

            migrationBuilder.AlterColumn<int>(
                name: "PriceRange",
                table: "Restaurants",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MainImageId",
                table: "Restaurants",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Franchises",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "GeneralTrustFactor",
                table: "User",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<long>(
                name: "AvatarId",
                table: "User",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ManagerState",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RestaurantId",
                table: "User",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_MainImageId",
                table: "Restaurants",
                column: "MainImageId",
                unique: true,
                filter: "[MainImageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_AvatarId",
                table: "User",
                column: "AvatarId",
                unique: true,
                filter: "[AvatarId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_RestaurantId",
                table: "User",
                column: "RestaurantId",
                unique: true,
                filter: "[RestaurantId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Image_RestaurantId",
                table: "Image",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Layouts_Restaurants_RestaurantId",
                table: "Layouts",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationVisitor_User_VisitorsId",
                table: "ReservationVisitor",
                column: "VisitorsId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Franchises_FranchiseId",
                table: "Restaurants",
                column: "FranchiseId",
                principalTable: "Franchises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Image_MainImageId",
                table: "Restaurants",
                column: "MainImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantVisitors_User_VisitorId",
                table: "RestaurantVisitors",
                column: "VisitorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Image_AvatarId",
                table: "User",
                column: "AvatarId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Restaurants_RestaurantId",
                table: "User",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitorFavourites_User_VisitorId",
                table: "VisitorFavourites",
                column: "VisitorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitorHistorys_User_VisitorId",
                table: "VisitorHistorys",
                column: "VisitorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Layouts_Restaurants_RestaurantId",
                table: "Layouts");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationVisitor_User_VisitorsId",
                table: "ReservationVisitor");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Franchises_FranchiseId",
                table: "Restaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Image_MainImageId",
                table: "Restaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantVisitors_User_VisitorId",
                table: "RestaurantVisitors");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Image_AvatarId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Restaurants_RestaurantId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitorFavourites_User_VisitorId",
                table: "VisitorFavourites");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitorHistorys_User_VisitorId",
                table: "VisitorHistorys");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_MainImageId",
                table: "Restaurants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_AvatarId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_RestaurantId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "MainImageId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Franchises");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ManagerState",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Visitors");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Visitors",
                newName: "Avatar");

            migrationBuilder.AddColumn<double>(
                name: "CoordX",
                table: "Tables",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CoordY",
                table: "Tables",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "WaiterId",
                table: "Tables",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PriceRange",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "CoordX",
                table: "Restaurants",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CoordY",
                table: "Restaurants",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "WaiterId",
                table: "Reservations",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "GeneralTrustFactor",
                table: "Visitors",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Visitors",
                table: "Visitors",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerState = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Managers_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Waiters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfServingTables = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<long>(type: "bigint", nullable: false),
                    WaiterState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Waiters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Waiters_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tables_WaiterId",
                table: "Tables",
                column: "WaiterId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_WaiterId",
                table: "Reservations",
                column: "WaiterId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_RestaurantId",
                table: "Managers",
                column: "RestaurantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Waiters_RestaurantId",
                table: "Waiters",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Layouts_Restaurants_RestaurantId",
                table: "Layouts",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Waiters_WaiterId",
                table: "Reservations",
                column: "WaiterId",
                principalTable: "Waiters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationVisitor_Visitors_VisitorsId",
                table: "ReservationVisitor",
                column: "VisitorsId",
                principalTable: "Visitors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Franchises_FranchiseId",
                table: "Restaurants",
                column: "FranchiseId",
                principalTable: "Franchises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantVisitors_Visitors_VisitorId",
                table: "RestaurantVisitors",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Waiters_WaiterId",
                table: "Tables",
                column: "WaiterId",
                principalTable: "Waiters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitorFavourites_Visitors_VisitorId",
                table: "VisitorFavourites",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitorHistorys_Visitors_VisitorId",
                table: "VisitorHistorys",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
