using Microsoft.EntityFrameworkCore.Migrations;

namespace BindNineCore.Migrations
{
    public partial class RemovedUselessColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expire",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "NegativeCacheTtl",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "Retry",
                table: "Domains");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Expire",
                table: "Domains",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NegativeCacheTtl",
                table: "Domains",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Retry",
                table: "Domains",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
