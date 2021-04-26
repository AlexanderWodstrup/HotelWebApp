using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelWepApp2.Data.Migrations
{
    public partial class GuestBuffetCheckIn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BuffetCheckIn",
                table: "Guests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuffetCheckIn",
                table: "Guests");
        }
    }
}
