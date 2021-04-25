using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelWepApp2.Data.Migrations
{
    public partial class Buffet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BuffetId",
                table: "Guests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Buffets",
                columns: table => new
                {
                    BuffetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuestType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buffets", x => x.BuffetId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Guests_BuffetId",
                table: "Guests",
                column: "BuffetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Buffets_BuffetId",
                table: "Guests",
                column: "BuffetId",
                principalTable: "Buffets",
                principalColumn: "BuffetId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Buffets_BuffetId",
                table: "Guests");

            migrationBuilder.DropTable(
                name: "Buffets");

            migrationBuilder.DropIndex(
                name: "IX_Guests_BuffetId",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "BuffetId",
                table: "Guests");
        }
    }
}
