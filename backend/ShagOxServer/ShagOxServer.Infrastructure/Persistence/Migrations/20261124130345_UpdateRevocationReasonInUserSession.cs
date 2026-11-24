using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRevocationReasonInUserSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSessions_UserDevices_DeviceId",
                table: "UserSessions");

            migrationBuilder.DropIndex(
                name: "IX_UserSessions_OwnerId_DeviceId",
                table: "UserSessions");

            migrationBuilder.AlterColumn<string>(
                name: "RefreshTokenHash",
                table: "UserSessions",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUsedAt",
                table: "UserSessions",
                type: "timestamptz",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RevocationReason",
                table: "UserSessions",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSessions_OwnerId_RevokedAt",
                table: "UserSessions",
                columns: new[] { "OwnerId", "RevokedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_UserSessions_RefreshTokenHash",
                table: "UserSessions",
                column: "RefreshTokenHash",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSessions_UserDevices_DeviceId",
                table: "UserSessions",
                column: "DeviceId",
                principalTable: "UserDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSessions_UserDevices_DeviceId",
                table: "UserSessions");

            migrationBuilder.DropIndex(
                name: "IX_UserSessions_OwnerId_RevokedAt",
                table: "UserSessions");

            migrationBuilder.DropIndex(
                name: "IX_UserSessions_RefreshTokenHash",
                table: "UserSessions");

            migrationBuilder.DropColumn(
                name: "LastUsedAt",
                table: "UserSessions");

            migrationBuilder.DropColumn(
                name: "RevocationReason",
                table: "UserSessions");

            migrationBuilder.AlterColumn<string>(
                name: "RefreshTokenHash",
                table: "UserSessions",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64);

            migrationBuilder.CreateIndex(
                name: "IX_UserSessions_OwnerId_DeviceId",
                table: "UserSessions",
                columns: new[] { "OwnerId", "DeviceId" },
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSessions_UserDevices_DeviceId",
                table: "UserSessions",
                column: "DeviceId",
                principalTable: "UserDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
