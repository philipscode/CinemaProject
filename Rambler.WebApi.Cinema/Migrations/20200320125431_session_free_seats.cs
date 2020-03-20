using Microsoft.EntityFrameworkCore.Migrations;

namespace Rambler.WebApi.Cinema.Migrations
{
    public partial class session_free_seats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FreeSeats",
                table: "Sessions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FreeSeats",
                table: "Sessions");
        }
    }
}
