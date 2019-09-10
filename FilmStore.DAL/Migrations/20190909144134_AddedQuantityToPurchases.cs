using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmStore.DAL.Migrations
{
    public partial class AddedQuantityToPurchases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "FilmPurchase",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "FilmPurchase");
        }
    }
}
