using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissionEntity_PermissionEntity_PermissionId",
                table: "RolePermissionEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissionEntity_Roles_RoleId",
                table: "RolePermissionEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleEntity_Roles_RoleId",
                table: "UserRoleEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleEntity_Users_UserId",
                table: "UserRoleEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoleEntity",
                table: "UserRoleEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermissionEntity",
                table: "RolePermissionEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PermissionEntity",
                table: "PermissionEntity");

            migrationBuilder.RenameTable(
                name: "UserRoleEntity",
                newName: "UserRoles");

            migrationBuilder.RenameTable(
                name: "RolePermissionEntity",
                newName: "RolePermissions");

            migrationBuilder.RenameTable(
                name: "PermissionEntity",
                newName: "Permissions");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoleEntity_UserId",
                table: "UserRoles",
                newName: "IX_UserRoles_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermissionEntity_PermissionId",
                table: "RolePermissions",
                newName: "IX_RolePermissions_PermissionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermissions",
                table: "RolePermissions",
                columns: new[] { "RoleId", "PermissionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permissions",
                table: "Permissions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Roles_RoleId",
                table: "RolePermissions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionId",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Roles_RoleId",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermissions",
                table: "RolePermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permissions",
                table: "Permissions");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "UserRoleEntity");

            migrationBuilder.RenameTable(
                name: "RolePermissions",
                newName: "RolePermissionEntity");

            migrationBuilder.RenameTable(
                name: "Permissions",
                newName: "PermissionEntity");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoleEntity",
                newName: "IX_UserRoleEntity_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissionEntity",
                newName: "IX_RolePermissionEntity_PermissionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoleEntity",
                table: "UserRoleEntity",
                columns: new[] { "RoleId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermissionEntity",
                table: "RolePermissionEntity",
                columns: new[] { "RoleId", "PermissionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PermissionEntity",
                table: "PermissionEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissionEntity_PermissionEntity_PermissionId",
                table: "RolePermissionEntity",
                column: "PermissionId",
                principalTable: "PermissionEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissionEntity_Roles_RoleId",
                table: "RolePermissionEntity",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleEntity_Roles_RoleId",
                table: "UserRoleEntity",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleEntity_Users_UserId",
                table: "UserRoleEntity",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
