using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tailor_Order_Management_System.Migrations
{
    public partial class dropOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
