using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_View_Member_MemberId",
                table: "View");

            migrationBuilder.DropForeignKey(
                name: "FK_View_Post_PostId",
                table: "View");

            migrationBuilder.DropPrimaryKey(
                name: "PK_View",
                table: "View");

            migrationBuilder.RenameTable(
                name: "View",
                newName: "Views");

            migrationBuilder.RenameIndex(
                name: "IX_View_PostId",
                table: "Views",
                newName: "IX_Views_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_View_MemberId",
                table: "Views",
                newName: "IX_Views_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Views",
                table: "Views",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Views_Member_MemberId",
                table: "Views",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Views_Post_PostId",
                table: "Views",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Views_Member_MemberId",
                table: "Views");

            migrationBuilder.DropForeignKey(
                name: "FK_Views_Post_PostId",
                table: "Views");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Views",
                table: "Views");

            migrationBuilder.RenameTable(
                name: "Views",
                newName: "View");

            migrationBuilder.RenameIndex(
                name: "IX_Views_PostId",
                table: "View",
                newName: "IX_View_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Views_MemberId",
                table: "View",
                newName: "IX_View_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_View",
                table: "View",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_View_Member_MemberId",
                table: "View",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_View_Post_PostId",
                table: "View",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id");
        }
    }
}
