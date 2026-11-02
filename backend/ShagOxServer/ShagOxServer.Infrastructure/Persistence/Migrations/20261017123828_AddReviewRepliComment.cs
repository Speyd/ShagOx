using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewRepliComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "ReviewComment",
                newName: "ReviewComments");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ReviewComments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<long>(
                name: "OwnerId",
                table: "ReviewComments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ReviewComments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReviewRepliComment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParentReplyId = table.Column<long>(type: "bigint", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    Text = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false),
                    CommentableEntityId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewRepliComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewRepliComment_ReviewComments_CommentableEntityId",
                        column: x => x.CommentableEntityId,
                        principalTable: "ReviewComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewRepliComment_ReviewRepliComment_ParentReplyId",
                        column: x => x.ParentReplyId,
                        principalTable: "ReviewRepliComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewRepliComment_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReviewRepliImage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    PublicId = table.Column<string>(type: "text", nullable: false),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewRepliImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewRepliImage_ReviewRepliComment_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "ReviewRepliComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewComments_CommentableEntityId_CreatedAt",
                table: "ReviewComments",
                columns: new[] { "CommentableEntityId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewComments_OwnerId",
                table: "ReviewComments",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRepliComment_CommentableEntityId",
                table: "ReviewRepliComment",
                column: "CommentableEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRepliComment_CommentableEntityId_CreatedAt",
                table: "ReviewRepliComment",
                columns: new[] { "CommentableEntityId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRepliComment_OwnerId",
                table: "ReviewRepliComment",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRepliComment_ParentReplyId",
                table: "ReviewRepliComment",
                column: "ParentReplyId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRepliComment_ParentReplyId_CreatedAt",
                table: "ReviewRepliComment",
                columns: new[] { "ParentReplyId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRepliImage_OwnerId",
                table: "ReviewRepliImage",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewComments_Users_OwnerId",
                table: "ReviewComments",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewComments_Users_OwnerId",
                table: "ReviewComments");

            migrationBuilder.DropTable(
                name: "ReviewRepliImage");

            migrationBuilder.DropTable(
                name: "ReviewRepliComment");

            migrationBuilder.DropIndex(
                name: "IX_ReviewComments_CommentableEntityId_CreatedAt",
                table: "ReviewComments");

            migrationBuilder.DropIndex(
                name: "IX_ReviewComments_OwnerId",
                table: "ReviewComments");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ReviewComments");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ReviewComments");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ReviewComments");

            migrationBuilder.RenameTable(
              name: "ReviewComments",
              newName: "ReviewComment");
        }
    }
}
