using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tailor_Order_Management_System.Migrations
{
    public partial class AddOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(nullable: true),
                    price = table.Column<int>(nullable: false),
                    quantity = table.Column<int>(nullable: false),
                    status = table.Column<string>(nullable: false),
                    OrderCode = table.Column<string>(nullable: false),
                    FabricColorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Orders_FabricColors_FabricColorId",
                        column: x => x.FabricColorId,
                        principalTable: "FabricColors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FabricColorId",
                table: "Orders",
                column: "FabricColorId");
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
