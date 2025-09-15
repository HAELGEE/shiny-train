using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Reported",
                table: "SubPost",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Reported",
                table: "Post",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Reports",
                table: "Member",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalLikes",
                table: "Member",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reported",
                table: "SubPost");

            migrationBuilder.DropColumn(
                name: "Reported",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "Reports",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "TotalLikes",
                table: "Member");
        }
    }
}
