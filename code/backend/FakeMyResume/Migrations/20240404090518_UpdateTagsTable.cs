using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeMyResume.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTagsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagName",
                table: "Tag");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tag",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_Name",
                table: "Tag",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tag_Name",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tag");

            migrationBuilder.AddColumn<string>(
                name: "TagName",
                table: "Tag",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
