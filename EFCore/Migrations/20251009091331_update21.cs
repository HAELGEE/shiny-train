using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    /// <inheritdoc />
    public partial class update21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Member_MemberId",
                table: "Post");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Member_MemberId",
                table: "Post",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Member_MemberId",
                table: "Post");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Member_MemberId",
                table: "Post",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id");
        }
    }
}
