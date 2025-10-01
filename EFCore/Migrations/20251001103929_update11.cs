using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    /// <inheritdoc />
    public partial class update11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AchivementId",
                table: "Member",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Achivements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Make10Post = table.Column<bool>(type: "bit", nullable: false),
                    Make10SubPost = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achivements", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Member_Achivements_AchivementId",
                table: "Member");

            migrationBuilder.DropTable(
                name: "Achivements");

            migrationBuilder.DropIndex(
                name: "IX_Member_AchivementId",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "AchivementId",
                table: "Member");
        }
    }
}
