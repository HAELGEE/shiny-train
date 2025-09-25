using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChattMember");

            migrationBuilder.AddColumn<int>(
                name: "ChattId",
                table: "Member",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Member_ChattId",
                table: "Member",
                column: "ChattId");

            migrationBuilder.CreateIndex(
                name: "IX_Chatt_ReceiverId",
                table: "Chatt",
                column: "ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chatt_Member_ReceiverId",
                table: "Chatt",
                column: "ReceiverId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Chatt_ChattId",
                table: "Member",
                column: "ChattId",
                principalTable: "Chatt",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chatt_Member_ReceiverId",
                table: "Chatt");

            migrationBuilder.DropForeignKey(
                name: "FK_Member_Chatt_ChattId",
                table: "Member");

            migrationBuilder.DropIndex(
                name: "IX_Member_ChattId",
                table: "Member");

            migrationBuilder.DropIndex(
                name: "IX_Chatt_ReceiverId",
                table: "Chatt");

            migrationBuilder.DropColumn(
                name: "ChattId",
                table: "Member");

            migrationBuilder.CreateTable(
                name: "ChattMember",
                columns: table => new
                {
                    ChattId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChattMember", x => new { x.ChattId, x.MemberId });
                    table.ForeignKey(
                        name: "FK_ChattMember_Chatt_ChattId",
                        column: x => x.ChattId,
                        principalTable: "Chatt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChattMember_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChattMember_MemberId",
                table: "ChattMember",
                column: "MemberId");
        }
    }
}
