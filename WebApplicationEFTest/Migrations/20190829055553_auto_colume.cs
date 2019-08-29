using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationEFTest.Migrations
{
    public partial class auto_colume : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserRole",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserRole",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "UserRole",
                newName: "role_id");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                newName: "IX_UserRole_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                newName: "IX_UserRole_role_id");

            migrationBuilder.RenameColumn(
                name: "Sex",
                table: "User",
                newName: "sex");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "User",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "User",
                newName: "age");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "User",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Role",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Role",
                newName: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "UserRole",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "UserRole",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "UserRole",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_user_id",
                table: "UserRole",
                newName: "IX_UserRole_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_role_id",
                table: "UserRole",
                newName: "IX_UserRole_RoleId");

            migrationBuilder.RenameColumn(
                name: "sex",
                table: "User",
                newName: "Sex");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "User",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "age",
                table: "User",
                newName: "Age");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "User",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Role",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Role",
                newName: "Id");
        }
    }
}
