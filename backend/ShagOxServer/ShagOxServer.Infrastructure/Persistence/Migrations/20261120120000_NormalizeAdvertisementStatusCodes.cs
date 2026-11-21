using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <summary>
    /// Status.Code was changed from free-form string to the
    /// AdvertisementStatus enum (stored as string). Brings existing
    /// rows in line with the enum: renames legacy lowercase codes,
    /// adds missing enum codes and removes codes the enum doesn't know.
    /// </summary>
    public partial class NormalizeAdvertisementStatusCodes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Legacy code -> enum code. If the enum code already exists,
            //    adverts are moved to it and the legacy row is removed.
            migrationBuilder.Sql("""
                DO $$
                DECLARE
                    m record;
                    s record;
                    target_id bigint;
                BEGIN
                    FOR m IN
                        SELECT * FROM (VALUES
                            (1, 'published', 'Active'),
                            (2, 'active',    'Active'),
                            (3, 'draft',     'Draft'),
                            (4, 'inactive',  'Hidden')
                        ) AS v(ord, legacy, code)
                        ORDER BY ord
                    LOOP
                        FOR s IN
                            SELECT "Id" FROM "Statuses"
                            WHERE "Code" = m.legacy
                            ORDER BY "Id"
                        LOOP
                            SELECT "Id" INTO target_id
                            FROM "Statuses"
                            WHERE "Code" = m.code
                            ORDER BY "Id"
                            LIMIT 1;

                            IF target_id IS NULL THEN
                                UPDATE "Statuses"
                                SET "Code" = m.code
                                WHERE "Id" = s."Id";
                            ELSE
                                UPDATE "Advertisements"
                                SET "StatusId" = target_id
                                WHERE "StatusId" = s."Id";

                                DELETE FROM "Statuses"
                                WHERE "Id" = s."Id";
                            END IF;
                        END LOOP;
                    END LOOP;
                END $$;
                """);

            // 2. Make sure every enum value has a row.
            migrationBuilder.Sql("""
                INSERT INTO "Statuses" ("Code", "EntityStatus")
                SELECT v.code, 'Active'
                FROM (VALUES ('Active'), ('Hidden'), ('Draft'), ('Archived'), ('Sold')) AS v(code)
                WHERE NOT EXISTS (
                    SELECT 1 FROM "Statuses" s WHERE s."Code" = v.code);
                """);

            // 3. Translations for statuses that have none for a language.
            migrationBuilder.Sql("""
                INSERT INTO "StatusTranslations"
                    ("TranslatableId", "Language", "Name", "Description", "EntityStatus")
                SELECT s."Id", t.lang, t.name, t.descr, 'Active'
                FROM (VALUES
                    ('Active',   'uk', 'Активний',     'Оголошення опубліковане'),
                    ('Active',   'en', 'Active',       'Advertisement is published'),
                    ('Active',   'de', 'Aktiv',        'Anzeige ist veröffentlicht'),
                    ('Hidden',   'uk', 'Прихований',   'Оголошення приховане'),
                    ('Hidden',   'en', 'Hidden',       'Advertisement is hidden'),
                    ('Hidden',   'de', 'Versteckt',    'Anzeige ist versteckt'),
                    ('Draft',    'uk', 'Чернетка',     'Статус чернетки'),
                    ('Draft',    'en', 'Draft',        'Status is a draft'),
                    ('Draft',    'de', 'Entwurf',      'Status ist ein Entwurf'),
                    ('Archived', 'uk', 'Архівований',  'Оголошення в архіві'),
                    ('Archived', 'en', 'Archived',     'Advertisement is archived'),
                    ('Archived', 'de', 'Archiviert',   'Anzeige ist archiviert'),
                    ('Sold',     'uk', 'Проданий',     'Товар продано'),
                    ('Sold',     'en', 'Sold',         'Item has been sold'),
                    ('Sold',     'de', 'Verkauft',     'Artikel wurde verkauft')
                ) AS t(code, lang, name, descr)
                JOIN "Statuses" s ON s."Code" = t.code
                WHERE NOT EXISTS (
                    SELECT 1 FROM "StatusTranslations" st
                    WHERE st."TranslatableId" = s."Id"
                      AND st."Language" = t.lang);
                """);

            // 4. Remove codes the enum doesn't know (pending, cancelled,
            //    completed, rejected, ...). Adverts still using them are
            //    hidden; translations go away via ON DELETE CASCADE.
            migrationBuilder.Sql("""
                UPDATE "Advertisements"
                SET "StatusId" = (
                    SELECT "Id" FROM "Statuses"
                    WHERE "Code" = 'Hidden'
                    ORDER BY "Id"
                    LIMIT 1)
                WHERE "StatusId" IN (
                    SELECT "Id" FROM "Statuses"
                    WHERE "Code" NOT IN ('Active', 'Hidden', 'Draft', 'Archived', 'Sold'));

                DELETE FROM "Statuses"
                WHERE "Code" NOT IN ('Active', 'Hidden', 'Draft', 'Archived', 'Sold');
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Deleted legacy statuses can't be restored; only the
            // renames are reverted.
            migrationBuilder.Sql("""
                UPDATE "Statuses" SET "Code" = 'published' WHERE "Code" = 'Active';
                UPDATE "Statuses" SET "Code" = 'draft'     WHERE "Code" = 'Draft';
                UPDATE "Statuses" SET "Code" = 'inactive'  WHERE "Code" = 'Hidden';
                """);
        }
    }
}
