using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flare.DataAccess.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PostUrls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContentPath",
                table: "Posts",
                newName: "Urls_Thumbnail");

            migrationBuilder.AddColumn<string>(
                name: "Urls_Fullscreen",
                table: "Posts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Urls_Original",
                table: "Posts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Urls_Fullscreen",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Urls_Original",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "Urls_Thumbnail",
                table: "Posts",
                newName: "ContentPath");
        }
    }
}
