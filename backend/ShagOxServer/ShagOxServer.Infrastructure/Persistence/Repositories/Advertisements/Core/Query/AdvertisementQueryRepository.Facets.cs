using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Advertisements.Core.Query;
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
    private sealed class FacetRow
    {
        public string Key { get; set; } = "";
        public string Value { get; set; } = "";
        public int Count { get; set; }
    }

    public async Task<AdvertisementFacetsDto> GetFacetsAsync(
        long categoryId,
        CancellationToken cancellationToken = default)
    {
        var rows = await _db.Database.SqlQuery<FacetRow>($"""
            with ads as (
                select "Id", "Attributes" from "Advertisements"
                where "CategoryId" = {categoryId} and "EntityStatus" = 'Active'
            ),
            attrs as (
                select a."Id" as id, e.key, e.value from ads a, jsonb_each(a."Attributes") e
                union all
                select a."Id", e.key, e.value from ads a
                join "AdvertisementVariants" v on v."AdvertisementId" = a."Id" and v."EntityStatus" = 'Active'
                cross join jsonb_each(v."Attributes") e
            ),
            flat as (
                select id, key, coalesce(el.value, attrs.value) as value from attrs
                left join lateral jsonb_array_elements(
                    case when jsonb_typeof(attrs.value) = 'array' then attrs.value end) el on true
            )
            select key as "Key", value::text as "Value", count(distinct id)::int as "Count"
            from flat group by key, value
            """).ToListAsync(cancellationToken);

        var prices = await _db.AdvertisementVariants
            .Where(v => v.Advertisement.CategoryId == categoryId
                && v.Advertisement.EntityStatus == EntityStatus.Active
                && v.EntityStatus == EntityStatus.Active)
            .GroupBy(_ => 1)
            .Select(g => new { Min = g.Min(v => v.Price), Max = g.Max(v => v.Price) })
            .FirstOrDefaultAsync(cancellationToken);

        return new AdvertisementFacetsDto(
            prices?.Min,
            prices?.Max,
            rows.Select(r => new AttributeFacetDto(r.Key, r.Value, r.Count)).ToList());
    }
}
