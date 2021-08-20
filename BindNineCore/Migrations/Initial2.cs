using Microsoft.EntityFrameworkCore.Migrations;

namespace BindNineCore.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Expire",
                table: "DomainService",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Nameserver1",
                table: "DomainService",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Nameserver2",
                table: "DomainService",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "NegativeCacheTtl",
                table: "DomainService",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Retry",
                table: "DomainService",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expire",
                table: "DomainService");

            migrationBuilder.DropColumn(
                name: "Nameserver1",
                table: "DomainService");

            migrationBuilder.DropColumn(
                name: "Nameserver2",
                table: "DomainService");

            migrationBuilder.DropColumn(
                name: "NegativeCacheTtl",
                table: "DomainService");

            migrationBuilder.DropColumn(
                name: "Retry",
                table: "DomainService");
        }
    }
}
