using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationEFTest.Migrations
{
    public partial class foeignkey_test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoginLog",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    user_id = table.Column<string>(nullable: true),
                    login_time = table.Column<DateTime>(nullable: false),
                    logout_time = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginLog", x => x.id);
                    table.ForeignKey(
                        name: "ForeignKey_LoginLog_User_User",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OperateLog",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    user_id1 = table.Column<string>(nullable: true),
                    user_id2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperateLog", x => x.id);
                    table.ForeignKey(
                        name: "ForeignKey_OperateLog_User_User1",
                        column: x => x.user_id1,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ForeignKey_OperateLog_User_User2",
                        column: x => x.user_id2,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoginLog_user_id",
                table: "LoginLog",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_OperateLog_user_id1",
                table: "OperateLog",
                column: "user_id1");

            migrationBuilder.CreateIndex(
                name: "IX_OperateLog_user_id2",
                table: "OperateLog",
                column: "user_id2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginLog");

            migrationBuilder.DropTable(
                name: "OperateLog");
        }
    }
}
