using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    /// <inheritdoc />
    public partial class update12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Member_Achivements_AchivementId",
                table: "Member");

            migrationBuilder.DropIndex(
                name: "IX_Member_AchivementId",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "AchivementId",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "Make10Post",
                table: "Achivements");

            migrationBuilder.DropColumn(
                name: "Make10SubPost",
                table: "Achivements");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Achivements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "Achivements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MemderId",
                table: "Achivements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Achivements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Achivements_MemberId",
                table: "Achivements",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Achivements_Member_MemberId",
                table: "Achivements",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achivements_Member_MemberId",
                table: "Achivements");

            migrationBuilder.DropIndex(
                name: "IX_Achivements_MemberId",
                table: "Achivements");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Achivements");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Achivements");

            migrationBuilder.DropColumn(
                name: "MemderId",
                table: "Achivements");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Achivements");

            migrationBuilder.AddColumn<int>(
                name: "AchivementId",
                table: "Member",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Make10Post",
                table: "Achivements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Make10SubPost",
                table: "Achivements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Member_AchivementId",
                table: "Member",
                column: "AchivementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Achivements_AchivementId",
                table: "Member",
                column: "AchivementId",
                principalTable: "Achivements",
                principalColumn: "Id");
        }
    }
}
