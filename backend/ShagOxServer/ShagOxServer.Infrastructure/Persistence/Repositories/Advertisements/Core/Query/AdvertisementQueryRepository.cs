using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core.Query;
public partial class AdvertisementQueryRepository 
: SearchRepository<Advertisement, AdvertisementSearchFilter>, 
      IAdvertisementQueryRepository
{
    public AdvertisementQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }

    public async Task<List<AdvertisementAttributeFilterOptionDto>>
        GetAttributeFilterOptionsAsync(
            long categoryId,
            CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT DISTINCT attribute.key AS "Key",
                   attribute.value #>> '{}' AS "Value",
                   FALSE AS "IsVariant"
            FROM "Advertisements" AS advertisement
            CROSS JOIN LATERAL jsonb_each(
                COALESCE(advertisement."Attributes", '{}'::jsonb)
            ) AS attribute(key, value)
            WHERE advertisement."CategoryId" = @categoryId

            UNION

            SELECT DISTINCT attribute.key AS "Key",
                   attribute.value #>> '{}' AS "Value",
                   TRUE AS "IsVariant"
            FROM "Advertisements" AS advertisement
            INNER JOIN "AdvertisementVariants" AS variant
                ON variant."AdvertisementId" = advertisement."Id"
            CROSS JOIN LATERAL jsonb_each(
                COALESCE(variant."Attributes", '{}'::jsonb)
            ) AS attribute(key, value)
            WHERE advertisement."CategoryId" = @categoryId

            ORDER BY "Key", "IsVariant", "Value"
            """;

        await _db.Database.OpenConnectionAsync(cancellationToken);
        try
        {
            await using var command = _db.Database
                .GetDbConnection()
                .CreateCommand();
            command.CommandText = sql;

            var categoryIdParameter = command.CreateParameter();
            categoryIdParameter.ParameterName = "categoryId";
            categoryIdParameter.Value = categoryId;
            command.Parameters.Add(categoryIdParameter);

            await using var reader = await command.ExecuteReaderAsync(
                cancellationToken);
            var options = new List<AdvertisementAttributeFilterOptionDto>();

            while (await reader.ReadAsync(cancellationToken))
            {
                if (reader.IsDBNull(1))
                    continue;

                options.Add(new AdvertisementAttributeFilterOptionDto(
                    reader.GetString(0),
                    reader.GetString(1),
                    reader.GetBoolean(2)));
            }

            return options;
        }
        finally
        {
            await _db.Database.CloseConnectionAsync();
        }
    }

    public async Task<List<Advertisement>> GetByIdsAsync(
        List<long> ids,
        CancellationToken cancellationToken = default)
    {
        return await _db.Advertisements
            .WithIncludes()
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<PagedResult<Advertisement>> GetBySellerAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default)
    {
        return await _db.Advertisements
            .WithIncludes()
            .Where(x => x.SellerId == userId)
            .OrderByDescending(x => x.Popularity)
            .ToPagedResultAsync(pagination,
                (x => x.Id),
                cancellationToken);
    }
    public async Task<PagedResult<Advertisement>> GetPurchasedByUserAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default)
    {
        return await _db.Advertisements
            .WithIncludes()
            .Where(x => x.BuyerId == userId)
            .OrderByDescending(x => x.Popularity)
            .ToPagedResultAsync(pagination,
                (x => x.Id),
                cancellationToken);
    }

    public override async Task<PagedResult<Advertisement>> SearchAsync(
        AdvertisementSearchFilter filter,
        PaginationParams pagination,
        CancellationToken cancellationToken = default)
    {
        return await _db.Advertisements
            .WithIncludes()
            .Filter(filter)
            .OrderByDescending(x => x.Popularity)
            .ToPagedResultAsync(pagination,
                (x => x.Id),
                cancellationToken);
    }
}
