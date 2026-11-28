using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexesInUserDeviceAndSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserSessions_OwnerId_RevokedAt_ExpiresAt",
                table: "UserSessions",
                columns: new[] { "OwnerId", "RevokedAt", "ExpiresAt" });

            migrationBuilder.CreateIndex(
                name: "IX_UserDevices_BlockedUntil",
                table: "UserDevices",
                column: "BlockedUntil",
                filter: "\"Status\" = 'Blocked' AND \"BlockedUntil\" IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserSessions_OwnerId_RevokedAt_ExpiresAt",
                table: "UserSessions");

            migrationBuilder.DropIndex(
                name: "IX_UserDevices_BlockedUntil",
                table: "UserDevices");
        }
    }
}
