using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmStore.DAL.Migrations
{
    public partial class AddedQuantityToFilm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuantityInStock",
                table: "Films",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantityInStock",
                table: "Films");
        }
    }
}
