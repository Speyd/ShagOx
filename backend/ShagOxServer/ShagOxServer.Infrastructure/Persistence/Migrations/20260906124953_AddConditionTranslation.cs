using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConditionTranslation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CityTranslations_Cities_CityId",
                table: "CityTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_RegionTranslations_Regions_RegionId",
                table: "RegionTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_StatusTranslations_Statuses_StatusId",
                table: "StatusTranslations");

            migrationBuilder.DropIndex(
                name: "IX_Cities_Code",
                table: "Cities");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "StatusTranslations",
                newName: "TranslatableId");

            migrationBuilder.RenameIndex(
                name: "IX_StatusTranslations_StatusId",
                table: "StatusTranslations",
                newName: "IX_StatusTranslations_TranslatableId");

            migrationBuilder.RenameColumn(
                name: "RegionId",
                table: "RegionTranslations",
                newName: "TranslatableId");

            migrationBuilder.RenameIndex(
                name: "IX_RegionTranslations_RegionId",
                table: "RegionTranslations",
                newName: "IX_RegionTranslations_TranslatableId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Conditions",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "CityTranslations",
                newName: "TranslatableId");

            migrationBuilder.RenameIndex(
                name: "IX_CityTranslations_CityId",
                table: "CityTranslations",
                newName: "IX_CityTranslations_TranslatableId");

            migrationBuilder.CreateTable(
                name: "ConditionTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TranslatableId = table.Column<int>(type: "integer", nullable: false),
                    Language = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConditionTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConditionTranslations_Conditions_TranslatableId",
                        column: x => x.TranslatableId,
                        principalTable: "Conditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conditions_Code",
                table: "Conditions",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Code",
                table: "Cities",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConditionTranslations_Language_Name",
                table: "ConditionTranslations",
                columns: new[] { "Language", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConditionTranslations_TranslatableId",
                table: "ConditionTranslations",
                column: "TranslatableId");

            migrationBuilder.AddForeignKey(
                name: "FK_CityTranslations_Cities_TranslatableId",
                table: "CityTranslations",
                column: "TranslatableId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegionTranslations_Regions_TranslatableId",
                table: "RegionTranslations",
                column: "TranslatableId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StatusTranslations_Statuses_TranslatableId",
                table: "StatusTranslations",
                column: "TranslatableId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Statuses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CityTranslations_Cities_TranslatableId",
                table: "CityTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_RegionTranslations_Regions_TranslatableId",
                table: "RegionTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_StatusTranslations_Statuses_TranslatableId",
                table: "StatusTranslations");

            migrationBuilder.DropTable(
                name: "ConditionTranslations");

            migrationBuilder.DropIndex(
                name: "IX_Conditions_Code",
                table: "Conditions");

            migrationBuilder.DropIndex(
                name: "IX_Cities_Code",
                table: "Cities");

            migrationBuilder.RenameColumn(
                name: "TranslatableId",
                table: "StatusTranslations",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_StatusTranslations_TranslatableId",
                table: "StatusTranslations",
                newName: "IX_StatusTranslations_StatusId");

            migrationBuilder.RenameColumn(
                name: "TranslatableId",
                table: "RegionTranslations",
                newName: "RegionId");

            migrationBuilder.RenameIndex(
                name: "IX_RegionTranslations_TranslatableId",
                table: "RegionTranslations",
                newName: "IX_RegionTranslations_RegionId");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Conditions",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "TranslatableId",
                table: "CityTranslations",
                newName: "CityId");

            migrationBuilder.RenameIndex(
                name: "IX_CityTranslations_TranslatableId",
                table: "CityTranslations",
                newName: "IX_CityTranslations_CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Code",
                table: "Cities",
                column: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_CityTranslations_Cities_CityId",
                table: "CityTranslations",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegionTranslations_Regions_RegionId",
                table: "RegionTranslations",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StatusTranslations_Statuses_StatusId",
                table: "StatusTranslations",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
