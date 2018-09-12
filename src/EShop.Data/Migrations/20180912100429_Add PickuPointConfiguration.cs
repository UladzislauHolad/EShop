using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Data.Migrations
{
    public partial class AddPickuPointConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PickupPoint_PickupPointId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PickupPoint",
                table: "PickupPoint");

            migrationBuilder.RenameTable(
                name: "PickupPoint",
                newName: "PickupPoints");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PickupPoints",
                table: "PickupPoints",
                column: "PickupPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PickupPoints_PickupPointId",
                table: "Orders",
                column: "PickupPointId",
                principalTable: "PickupPoints",
                principalColumn: "PickupPointId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PickupPoints_PickupPointId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PickupPoints",
                table: "PickupPoints");

            migrationBuilder.RenameTable(
                name: "PickupPoints",
                newName: "PickupPoint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PickupPoint",
                table: "PickupPoint",
                column: "PickupPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PickupPoint_PickupPointId",
                table: "Orders",
                column: "PickupPointId",
                principalTable: "PickupPoint",
                principalColumn: "PickupPointId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
