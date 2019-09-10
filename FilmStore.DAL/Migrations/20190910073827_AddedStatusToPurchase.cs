using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmStore.DAL.Migrations
{
    public partial class AddedStatusToPurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "FilmPurchase");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Purchases",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Purchases");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "FilmPurchase",
                nullable: false,
                defaultValue: 0);
        }
    }
}
