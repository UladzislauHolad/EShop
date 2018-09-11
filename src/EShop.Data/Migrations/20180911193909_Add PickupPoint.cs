using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Data.Migrations
{
    public partial class AddPickupPoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PickupPointId",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PickupPoint",
                columns: table => new
                {
                    PickupPointId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickupPoint", x => x.PickupPointId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PickupPointId",
                table: "Orders",
                column: "PickupPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PickupPoint_PickupPointId",
                table: "Orders",
                column: "PickupPointId",
                principalTable: "PickupPoint",
                principalColumn: "PickupPointId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PickupPoint_PickupPointId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "PickupPoint");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PickupPointId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PickupPointId",
                table: "Orders");
        }
    }
}
