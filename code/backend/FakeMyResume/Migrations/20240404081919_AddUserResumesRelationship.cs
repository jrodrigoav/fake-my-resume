using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeMyResume.Migrations
{
    /// <inheritdoc />
    public partial class AddUserResumesRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataResume_User_UserId",
                table: "DataResume");

            migrationBuilder.DropIndex(
                name: "IX_DataResume_UserId",
                table: "DataResume");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DataResume");

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "DataResume",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_DataResume_AccountId",
                table: "DataResume",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_DataResume_User_AccountId",
                table: "DataResume",
                column: "AccountId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataResume_User_AccountId",
                table: "DataResume");

            migrationBuilder.DropIndex(
                name: "IX_DataResume_AccountId",
                table: "DataResume");

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "DataResume",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "DataResume",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DataResume_UserId",
                table: "DataResume",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DataResume_User_UserId",
                table: "DataResume",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
