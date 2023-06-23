using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaintBarnabasHouse.Migrations
{
    public partial class MainImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainImgUrl",
                table: "Products",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImgUrl",
                table: "Products");
        }
    }
}
