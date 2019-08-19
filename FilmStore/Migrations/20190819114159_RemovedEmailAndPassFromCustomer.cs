using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmStore.Migrations
{
    public partial class RemovedEmailAndPassFromCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Customers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Customers",
                nullable: true);
        }
    }
}
