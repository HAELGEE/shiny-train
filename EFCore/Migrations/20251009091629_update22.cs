using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    /// <inheritdoc />
    public partial class update22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Post_PostId",
                table: "Reports");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Post_PostId",
                table: "Reports",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Post_PostId",
                table: "Reports");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Post_PostId",
                table: "Reports",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id");
        }
    }
}
