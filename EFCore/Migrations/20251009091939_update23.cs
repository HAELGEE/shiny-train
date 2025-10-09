using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    /// <inheritdoc />
    public partial class update23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubPost_Post_PostId",
                table: "SubPost");

            migrationBuilder.AddForeignKey(
                name: "FK_SubPost_Post_PostId",
                table: "SubPost",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubPost_Post_PostId",
                table: "SubPost");

            migrationBuilder.AddForeignKey(
                name: "FK_SubPost_Post_PostId",
                table: "SubPost",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id");
        }
    }
}
