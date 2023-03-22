using Microsoft.EntityFrameworkCore.Migrations;

namespace TableTracker.Infrastructure.Migrations
{
    public partial class ChangeWaiterVisitor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Waiters_ServingWaiterId",
                table: "Tables");

            migrationBuilder.DropForeignKey(
                name: "FK_Waiters_Visitors_VisitorId",
                table: "Waiters");

            migrationBuilder.DropIndex(
                name: "IX_Waiters_VisitorId",
                table: "Waiters");

            migrationBuilder.DropIndex(
                name: "IX_Tables_ServingWaiterId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "VisitorId",
                table: "Waiters");

            migrationBuilder.DropColumn(
                name: "ServingWaiterId",
                table: "Tables");

            migrationBuilder.AddColumn<long>(
                name: "WaiterId",
                table: "Tables",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WaiterId",
                table: "Reservations",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tables_WaiterId",
                table: "Tables",
                column: "WaiterId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_WaiterId",
                table: "Reservations",
                column: "WaiterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Waiters_WaiterId",
                table: "Reservations",
                column: "WaiterId",
                principalTable: "Waiters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Waiters_WaiterId",
                table: "Tables",
                column: "WaiterId",
                principalTable: "Waiters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Waiters_WaiterId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Waiters_WaiterId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_WaiterId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_WaiterId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "WaiterId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "WaiterId",
                table: "Reservations");

            migrationBuilder.AddColumn<long>(
                name: "VisitorId",
                table: "Waiters",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ServingWaiterId",
                table: "Tables",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Waiters_VisitorId",
                table: "Waiters",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_ServingWaiterId",
                table: "Tables",
                column: "ServingWaiterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Waiters_ServingWaiterId",
                table: "Tables",
                column: "ServingWaiterId",
                principalTable: "Waiters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Waiters_Visitors_VisitorId",
                table: "Waiters",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
