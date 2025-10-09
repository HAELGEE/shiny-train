using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    /// <inheritdoc />
    public partial class update20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostViews_Post_PostId",
                table: "PostViews");

            migrationBuilder.AddForeignKey(
                name: "FK_PostViews_Post_PostId",
                table: "PostViews",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostViews_Post_PostId",
                table: "PostViews");

            migrationBuilder.AddForeignKey(
                name: "FK_PostViews_Post_PostId",
                table: "PostViews",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id");
        }
    }
}
