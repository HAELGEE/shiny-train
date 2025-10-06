using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    /// <inheritdoc />
    public partial class update16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReporterId",
                table: "SubPost",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubPostId",
                table: "Reports",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_SubPostId",
                table: "Reports",
                column: "SubPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_SubPost_SubPostId",
                table: "Reports",
                column: "SubPostId",
                principalTable: "SubPost",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_SubPost_SubPostId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_SubPostId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ReporterId",
                table: "SubPost");

            migrationBuilder.DropColumn(
                name: "SubPostId",
                table: "Reports");
        }
    }
}
