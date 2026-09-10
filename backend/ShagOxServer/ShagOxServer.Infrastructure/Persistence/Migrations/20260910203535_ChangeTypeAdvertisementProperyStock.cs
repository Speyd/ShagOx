using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTypeAdvertisementProperyStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER TABLE "Advertisements"
                ALTER COLUMN "Stock" DROP DEFAULT;

                ALTER TABLE "Advertisements"
                ALTER COLUMN "Stock" TYPE integer
                USING CASE
                    WHEN "Stock" = TRUE THEN 1
                    ELSE 0
                END;

                ALTER TABLE "Advertisements"
                ALTER COLUMN "Stock" SET DEFAULT 0;

                ALTER TABLE "Advertisements"
                ALTER COLUMN "Stock" SET NOT NULL;
            """);
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Stock",
                table: "Advertisements",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);
        }
    }
}
