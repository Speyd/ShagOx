using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePropertiesUserDevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDevices_Users_UserId",
                table: "UserDevices");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSessions_Users_UserId",
                table: "UserSessions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserSessions",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSessions_UserId_DeviceId",
                table: "UserSessions",
                newName: "IX_UserSessions_OwnerId_DeviceId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserDevices",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_UserDevices_UserId_DeviceIdentifier",
                table: "UserDevices",
                newName: "IX_UserDevices_OwnerId_DeviceIdentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDevices_Users_OwnerId",
                table: "UserDevices",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSessions_Users_OwnerId",
                table: "UserSessions",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDevices_Users_OwnerId",
                table: "UserDevices");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSessions_Users_OwnerId",
                table: "UserSessions");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "UserSessions",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSessions_OwnerId_DeviceId",
                table: "UserSessions",
                newName: "IX_UserSessions_UserId_DeviceId");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "UserDevices",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserDevices_OwnerId_DeviceIdentifier",
                table: "UserDevices",
                newName: "IX_UserDevices_UserId_DeviceIdentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDevices_Users_UserId",
                table: "UserDevices",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSessions_Users_UserId",
                table: "UserSessions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
