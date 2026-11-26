using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeviceAndUserDeviceAndUserSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserDevices_OwnerId_DeviceIdentifier",
                table: "UserDevices");

            migrationBuilder.DropColumn(
                name: "Browser",
                table: "UserDevices");

            migrationBuilder.DropColumn(
                name: "DeviceIdentifier",
                table: "UserDevices");

            migrationBuilder.DropColumn(
                name: "DeviceName",
                table: "UserDevices");

            migrationBuilder.DropColumn(
                name: "DeviceType",
                table: "UserDevices");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "UserDevices");

            migrationBuilder.DropColumn(
                name: "OperatingSystem",
                table: "UserDevices");

            migrationBuilder.DropColumn(
                name: "Browser",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "Devices");

            migrationBuilder.AddColumn<string>(
                name: "Browser",
                table: "UserSessions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BrowserVersion",
                table: "UserSessions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "UserSessions",
                type: "character varying(45)",
                maxLength: 45,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeviceId",
                table: "UserDevices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_UserDevices_DeviceId",
                table: "UserDevices",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDevices_OwnerId_DeviceId",
                table: "UserDevices",
                columns: new[] { "OwnerId", "DeviceId" },
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDevices_Devices_DeviceId",
                table: "UserDevices",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDevices_Devices_DeviceId",
                table: "UserDevices");

            migrationBuilder.DropIndex(
                name: "IX_UserDevices_DeviceId",
                table: "UserDevices");

            migrationBuilder.DropIndex(
                name: "IX_UserDevices_OwnerId_DeviceId",
                table: "UserDevices");

            migrationBuilder.DropColumn(
                name: "Browser",
                table: "UserSessions");

            migrationBuilder.DropColumn(
                name: "BrowserVersion",
                table: "UserSessions");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "UserSessions");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "UserDevices");

            migrationBuilder.AddColumn<string>(
                name: "Browser",
                table: "UserDevices",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeviceIdentifier",
                table: "UserDevices",
                type: "character varying(45)",
                maxLength: 45,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeviceName",
                table: "UserDevices",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeviceType",
                table: "UserDevices",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "UserDevices",
                type: "character varying(45)",
                maxLength: 45,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OperatingSystem",
                table: "UserDevices",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Browser",
                table: "Devices",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "Devices",
                type: "character varying(45)",
                maxLength: 45,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDevices_OwnerId_DeviceIdentifier",
                table: "UserDevices",
                columns: new[] { "OwnerId", "DeviceIdentifier" },
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");
        }
    }
}
