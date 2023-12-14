using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBlog.WebApp.Migrations
{
    public partial class NoteAddColumnIsSlider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isSlider",
                table: "Notes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isSlider",
                table: "Notes");
        }
    }
}
