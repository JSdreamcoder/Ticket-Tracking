using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProjectOfUnittest.Data.Migrations
{
    public partial class addfilepropertyToattechment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileUrl",
                table: "TicketAttachment");

            migrationBuilder.AddColumn<byte[]>(
                name: "file",
                table: "TicketAttachment",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "file",
                table: "TicketAttachment");

            migrationBuilder.AddColumn<string>(
                name: "FileUrl",
                table: "TicketAttachment",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
