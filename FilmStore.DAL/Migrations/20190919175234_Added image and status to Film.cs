using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmStore.DAL.Migrations
{
    public partial class AddedimageandstatustoFilm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Films",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Films",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Films");
        }
    }
}
