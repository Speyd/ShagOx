using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStatusInUserDevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSessions_UserDevices_DeviceId",
                table: "UserSessions");

            migrationBuilder.RenameColumn(
                name: "DeviceId",
                table: "UserSessions",
                newName: "UserDeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSessions_DeviceId",
                table: "UserSessions",
                newName: "IX_UserSessions_UserDeviceId");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "UserDevices",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BlockedUntil",
                table: "UserDevices",
                type: "timestamptz",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BlockedAt",
                table: "UserDevices",
                type: "timestamptz",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSessions_UserDevices_UserDeviceId",
                table: "UserSessions",
                column: "UserDeviceId",
                principalTable: "UserDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSessions_UserDevices_UserDeviceId",
                table: "UserSessions");

            migrationBuilder.RenameColumn(
                name: "UserDeviceId",
                table: "UserSessions",
                newName: "DeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSessions_UserDeviceId",
                table: "UserSessions",
                newName: "IX_UserSessions_DeviceId");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "UserDevices",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BlockedUntil",
                table: "UserDevices",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BlockedAt",
                table: "UserDevices",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSessions_UserDevices_DeviceId",
                table: "UserSessions",
                column: "DeviceId",
                principalTable: "UserDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
