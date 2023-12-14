using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBlog.WebApp.Migrations
{
    public partial class NoteAddColumnIsHome : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isHome",
                table: "Notes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isHome",
                table: "Notes");
        }
    }
}
