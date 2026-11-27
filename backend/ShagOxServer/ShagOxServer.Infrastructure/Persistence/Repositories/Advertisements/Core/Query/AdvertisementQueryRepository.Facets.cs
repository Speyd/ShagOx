using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Advertisements.Core.Query.Facets;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core.Query;
public partial class AdvertisementQueryRepository
    : SearchRepository<Advertisement, AdvertisementSearchFilter>,
      IAdvertisementQueryRepository
{
    public async Task<AdvertisementFacetsDto> GetFacetsAsync(
        long categoryId,
        CancellationToken cancellationToken = default)
    {
        var attributes = await GetAttributeFacetsAsync(
            categoryId,
            cancellationToken);

        var prices = await GetPriceRangeAsync(
            categoryId,
            cancellationToken);

        return new AdvertisementFacetsDto(
            prices?.Min,
            prices?.Max,
            attributes);
    }

    private async Task<List<AttributeFacetDto>> GetAttributeFacetsAsync(
        long categoryId,
        CancellationToken cancellationToken)
    {
        return await _db.Database.SqlQuery<AttributeFacetDto>($$"""
            WITH ads AS (
                SELECT "Id", "Attributes"
                FROM "Advertisements"
                WHERE "CategoryId" = {{categoryId}}
                  AND "EntityStatus" = 'Active'
            ),
            attributes AS (
                SELECT
                    a."Id" AS id,
                    attr.key,
                    attr.value
                FROM ads a
                CROSS JOIN LATERAL jsonb_each(a."Attributes") AS attr

                UNION ALL

                SELECT
                    a."Id" AS id,
                    attr.key,
                    attr.value
                FROM ads a
                INNER JOIN "AdvertisementVariants" v
                    ON v."AdvertisementId" = a."Id"
                   AND v."EntityStatus" = 'Active'
                CROSS JOIN LATERAL jsonb_each(v."Attributes") AS attr
            ),
            flat AS (
                SELECT
                    a.id,
                    a.key,
                    COALESCE(item.value, a.value) AS value
                FROM attributes a
                LEFT JOIN LATERAL jsonb_array_elements(
                    CASE
                        WHEN jsonb_typeof(a.value) = 'array'
                        THEN a.value
                    END
                ) AS item ON TRUE
            )
            SELECT
                key AS "Key",
                value::text AS "Value",
                COUNT(DISTINCT id)::int AS "Count"
            FROM flat
            GROUP BY key, value
            ORDER BY key, value
            """)
            .ToListAsync(cancellationToken);
    }

    private async Task<(decimal Min, decimal Max)?> GetPriceRangeAsync(
        long categoryId,
        CancellationToken cancellationToken)
    {
        var prices = await _db.AdvertisementVariants
            .Where(v =>
                v.Advertisement.CategoryId == categoryId &&
                v.Advertisement.EntityStatus == EntityStatus.Active &&
                v.EntityStatus == EntityStatus.Active)
            .GroupBy(_ => 1)
            .Select(g => new
            {
                Min = g.Min(v => v.Price),
                Max = g.Max(v => v.Price)
            })
            .FirstOrDefaultAsync(cancellationToken);

        return prices is null
            ? null
            : (prices.Min, prices.Max);
    }
}
