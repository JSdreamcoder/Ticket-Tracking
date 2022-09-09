using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProjectOfUnittest.Data.Migrations
{
    public partial class addIsOpenToTicketNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOpen",
                table: "TicketNotification",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOpen",
                table: "TicketNotification");
        }
    }
}
