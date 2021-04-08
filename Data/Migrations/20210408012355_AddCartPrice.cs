using Microsoft.EntityFrameworkCore.Migrations;

namespace Assignment1B.Data.Migrations
{
    public partial class AddCartPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Trips",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Trips",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Trips");
        }
    }
}
