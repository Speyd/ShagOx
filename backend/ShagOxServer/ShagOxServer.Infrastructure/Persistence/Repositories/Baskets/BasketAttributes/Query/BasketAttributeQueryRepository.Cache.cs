using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketAttributes;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketAttributes.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketAttributes.Query;
public partial class BasketAttributeQueryRepository
    : SearchRepository<BasketAttribute, BasketAttributeSearchFilter>,
      IBasketAttributeQueryRepository
{
    public async Task<List<BasketAttributeCacheInfo>> GetCacheInfosByCategoryAsync(
        long categoryId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.BasketAttributes
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(c =>
                c.AttributeDefinition.CategoryId == categoryId)
            .OrderBy(c => c.Order)
            .SelectCacheInfo()
            .ToListAsync(cancellationToken);
    }
    public async Task<BasketAttributeCacheInfo?> GetCacheInfoByAttributeDefinitionAsync(
        long attributeDefinitionId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.BasketAttributes
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x => x.AttributeDefinitionId == attributeDefinitionId)
            .SelectCacheInfo()
            .FirstOrDefaultAsync(cancellationToken);
    }
}
