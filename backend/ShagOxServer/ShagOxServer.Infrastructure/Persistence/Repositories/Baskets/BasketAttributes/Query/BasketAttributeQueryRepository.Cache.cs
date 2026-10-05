using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes.Query;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketAttributes;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketAttributes.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketAttributes.Query;
public partial class BasketAttributeQueryRepository
    : QueryRepository<BasketAttribute, BasketAttributeSearchFilter>,
      IBasketAttributeQueryRepository
{
    public async Task<List<BasketAttributeCacheInfo>> GetCacheInfosByCategoryAsync(
        long categoryId)
    {
        return await _db.BasketAttributes
            .WithIncludes()
            .Where(c =>
                c.AttributeDefinition.CategoryId == categoryId)
            .OrderBy(c => c.Order)
            .SelectCacheInfo()
            .ToListAsync();
    }
    public async Task<BasketAttributeCacheInfo?> GetCacheInfoByAttributeDefinitionAsync(
        long attributeDefinitionId)
    {
        return await _db.BasketAttributes
            .WithIncludes()
            .Where(x => x.AttributeDefinitionId == attributeDefinitionId)
            .SelectCacheInfo()
            .FirstOrDefaultAsync();
    }
}