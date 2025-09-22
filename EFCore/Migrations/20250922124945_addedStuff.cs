using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    /// <inheritdoc />
    public partial class addedStuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Views");

            migrationBuilder.CreateTable(
                name: "Chatt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: true),
                    ReceiverId = table.Column<int>(type: "int", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chatt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemberViews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: true),
                    VisitorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberViews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberViews_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MemberViews_Member_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostViews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: true),
                    PostId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostViews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostViews_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostViews_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_MemberViews_MemberId",
                table: "MemberViews",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberViews_VisitorId",
                table: "MemberViews",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_PostViews_MemberId",
                table: "PostViews",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_PostViews_PostId",
                table: "PostViews",
                column: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChattMember");

            migrationBuilder.DropTable(
                name: "MemberViews");

            migrationBuilder.DropTable(
                name: "PostViews");

            migrationBuilder.DropTable(
                name: "Chatt");

            migrationBuilder.CreateTable(
                name: "Views",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: true),
                    PostId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Views", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Views_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Views_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Views_MemberId",
                table: "Views",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Views_PostId",
                table: "Views",
                column: "PostId");
        }
    }
}
