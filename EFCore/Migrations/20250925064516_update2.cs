using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    /// <inheritdoc />
    public partial class update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Member_Chatt_ChattId",
                table: "Member");

            migrationBuilder.DropIndex(
                name: "IX_Member_ChattId",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "ChattId",
                table: "Member");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChattId",
                table: "Member",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Member_ChattId",
                table: "Member",
                column: "ChattId");

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Chatt_ChattId",
                table: "Member",
                column: "ChattId",
                principalTable: "Chatt",
                principalColumn: "Id");
        }
    }
}
