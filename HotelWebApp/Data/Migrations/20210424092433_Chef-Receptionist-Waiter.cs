using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelWebApp.Data.Migrations
{
    public partial class ChefReceptionistWaiter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChefId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Chef_Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceptionistId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Receptionist_Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WaiterId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChefId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Chef_Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ReceptionistId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Receptionist_Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WaiterId",
                table: "AspNetUsers");
        }
    }
}
