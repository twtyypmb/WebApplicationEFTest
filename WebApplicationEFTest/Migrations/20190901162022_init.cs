using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationEFTest.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    sex = table.Column<int>(nullable: false),
                    age = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    user_id_current = table.Column<string>(nullable: true),
                    user_id_parent = table.Column<string>(nullable: true),
                    role_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.id);
                    table.ForeignKey(
                        name: "ForeignKey_UserRole_Role_Role",
                        column: x => x.role_id,
                        principalTable: "Role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ForeignKey_UserRole_User_UserCurrent",
                        column: x => x.user_id_current,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ForeignKey_UserRole_User_UserParent",
                        column: x => x.user_id_parent,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_role_id",
                table: "UserRole",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_user_id_current",
                table: "UserRole",
                column: "user_id_current");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_user_id_parent",
                table: "UserRole",
                column: "user_id_parent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
