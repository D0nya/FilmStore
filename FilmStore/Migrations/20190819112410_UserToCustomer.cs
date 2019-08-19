using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmStore.Migrations
{
    public partial class UserToCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserRef",
                table: "Customers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserRef",
                table: "Customers",
                column: "UserRef",
                unique: true,
                filter: "[UserRef] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_UserRef",
                table: "Customers",
                column: "UserRef",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_UserRef",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_UserRef",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UserRef",
                table: "Customers");
        }
    }
}
