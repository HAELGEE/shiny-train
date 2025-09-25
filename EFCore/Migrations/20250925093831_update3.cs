using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    /// <inheritdoc />
    public partial class update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chatt_Member_ReceiverId",
                table: "Chatt");

            migrationBuilder.CreateIndex(
                name: "IX_Chatt_SenderId",
                table: "Chatt",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chatt_ReceiverMember",
                table: "Chatt",
                column: "ReceiverId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Chatt_SenderMember",
                table: "Chatt",
                column: "SenderId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chatt_ReceiverMember",
                table: "Chatt");

            migrationBuilder.DropForeignKey(
                name: "FK_Chatt_SenderMember",
                table: "Chatt");

            migrationBuilder.DropIndex(
                name: "IX_Chatt_SenderId",
                table: "Chatt");

            migrationBuilder.AddForeignKey(
                name: "FK_Chatt_Member_ReceiverId",
                table: "Chatt",
                column: "ReceiverId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
