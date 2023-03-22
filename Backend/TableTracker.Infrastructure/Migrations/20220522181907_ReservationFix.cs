using Microsoft.EntityFrameworkCore.Migrations;

namespace TableTracker.Infrastructure.Migrations
{
    public partial class ReservationFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationVisitor");

            migrationBuilder.AddColumn<long>(
                name: "VisitorId",
                table: "Reservations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_VisitorId",
                table: "Reservations",
                column: "VisitorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_User_VisitorId",
                table: "Reservations",
                column: "VisitorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_User_VisitorId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_VisitorId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "VisitorId",
                table: "Reservations");

            migrationBuilder.CreateTable(
                name: "ReservationVisitor",
                columns: table => new
                {
                    ReservationsId = table.Column<long>(type: "bigint", nullable: false),
                    VisitorsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationVisitor", x => new { x.ReservationsId, x.VisitorsId });
                    table.ForeignKey(
                        name: "FK_ReservationVisitor_Reservations_ReservationsId",
                        column: x => x.ReservationsId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationVisitor_User_VisitorsId",
                        column: x => x.VisitorsId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationVisitor_VisitorsId",
                table: "ReservationVisitor",
                column: "VisitorsId");
        }
    }
}
