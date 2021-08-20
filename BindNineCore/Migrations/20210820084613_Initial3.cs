using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BindNineCore.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DomainService",
                table: "DomainService");

            migrationBuilder.DropColumn(
                name: "Nameserver1",
                table: "DomainService");

            migrationBuilder.DropColumn(
                name: "Nameserver2",
                table: "DomainService");

            migrationBuilder.RenameTable(
                name: "DomainService",
                newName: "Domains");

            migrationBuilder.AlterColumn<string>(
                name: "Domain",
                table: "Domains",
                type: "varchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Domains",
                table: "Domains",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Dns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DomainId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Subdomain = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RecordType = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CanBeDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dns_Domains_DomainId",
                        column: x => x.DomainId,
                        principalTable: "Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Dns_DomainId",
                table: "Dns",
                column: "DomainId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Domains",
                table: "Domains");

            migrationBuilder.RenameTable(
                name: "Domains",
                newName: "DomainService");

            migrationBuilder.AlterColumn<string>(
                name: "Domain",
                table: "DomainService",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldMaxLength: 64,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_DomainService",
                table: "DomainService",
                column: "Id");
        }
    }
}
