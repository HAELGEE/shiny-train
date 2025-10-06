using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    /// <inheritdoc />
    public partial class update14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "SubPost",
                newName: "ParentSubpostId");

            migrationBuilder.AddColumn<int>(
                name: "ParentPostId",
                table: "SubPost",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentPostId",
                table: "SubPost");

            migrationBuilder.RenameColumn(
                name: "ParentSubpostId",
                table: "SubPost",
                newName: "ParentId");
        }
    }
}
