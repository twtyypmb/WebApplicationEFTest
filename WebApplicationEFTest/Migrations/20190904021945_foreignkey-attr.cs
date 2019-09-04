using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationEFTest.Migrations
{
    public partial class foreignkeyattr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "ForeignKey_LoginLog_User_User",
                table: "LoginLog");

            migrationBuilder.DropForeignKey(
                name: "ForeignKey_OperateLog_User_User1",
                table: "OperateLog");

            migrationBuilder.DropForeignKey(
                name: "ForeignKey_OperateLog_User_User2",
                table: "OperateLog");

            migrationBuilder.DropForeignKey(
                name: "ForeignKey_UserRole_Role_Role",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "ForeignKey_UserRole_User_UserCurrent",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "ForeignKey_UserRole_User_UserParent",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserExtraInfo_user_id",
                table: "UserExtraInfo");

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "UserExtraInfo",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserExtraInfo_user_id",
                table: "UserExtraInfo",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_LoginLog_User_user_id",
                table: "LoginLog",
                column: "user_id",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OperateLog_User_user_id1",
                table: "OperateLog",
                column: "user_id1",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OperateLog_User_user_id2",
                table: "OperateLog",
                column: "user_id2",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_role_id",
                table: "UserRole",
                column: "role_id",
                principalTable: "Role",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_user_id_current",
                table: "UserRole",
                column: "user_id_current",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_user_id_parent",
                table: "UserRole",
                column: "user_id_parent",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginLog_User_user_id",
                table: "LoginLog");

            migrationBuilder.DropForeignKey(
                name: "FK_OperateLog_User_user_id1",
                table: "OperateLog");

            migrationBuilder.DropForeignKey(
                name: "FK_OperateLog_User_user_id2",
                table: "OperateLog");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_role_id",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_user_id_current",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_user_id_parent",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserExtraInfo_user_id",
                table: "UserExtraInfo");

            migrationBuilder.DropColumn(
                name: "address",
                table: "UserExtraInfo");

            migrationBuilder.CreateIndex(
                name: "IX_UserExtraInfo_user_id",
                table: "UserExtraInfo",
                column: "user_id",
                unique: true,
                filter: "[user_id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "ForeignKey_LoginLog_User_User",
                table: "LoginLog",
                column: "user_id",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "ForeignKey_OperateLog_User_User1",
                table: "OperateLog",
                column: "user_id1",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "ForeignKey_OperateLog_User_User2",
                table: "OperateLog",
                column: "user_id2",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "ForeignKey_UserRole_Role_Role",
                table: "UserRole",
                column: "role_id",
                principalTable: "Role",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "ForeignKey_UserRole_User_UserCurrent",
                table: "UserRole",
                column: "user_id_current",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "ForeignKey_UserRole_User_UserParent",
                table: "UserRole",
                column: "user_id_parent",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
