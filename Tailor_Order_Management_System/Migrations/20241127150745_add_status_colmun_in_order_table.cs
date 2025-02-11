using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tailor_Order_Management_System.Migrations
{
    public partial class add_status_colmun_in_order_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Orders");
        }
    }
}
