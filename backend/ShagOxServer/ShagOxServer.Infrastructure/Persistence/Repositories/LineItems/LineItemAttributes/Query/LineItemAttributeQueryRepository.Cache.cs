using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.LineItems;
using ShagOxServer.Domain.Entities.LineItems;
using ShagOxServer.Domain.Filters.LineItems.LineItemAttributes;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.LineItems.LineItemAttributes.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.LineItems.LineItemAttributes.Query;
public partial class LineItemAttributeQueryRepository
    : SearchRepository<LineItemAttribute, LineItemAttributeSearchFilter>,
      ILineItemAttributeQueryRepository
{
    public async Task<List<LineItemAttributeCacheContext>> GetCacheInfosByCategoryAsync(
        long categoryId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        return await _db.LineItemAttributes
            .AsNoTracking()
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(c =>
                c.AttributeDefinition.CategoryId == categoryId)
            .OrderBy(c => c.Order)
            .SelectCacheInfo(status)
            .ToListAsync(cancellationToken);
    }
    public async Task<LineItemAttributeCacheContext?> GetCacheInfoByAttributeDefinitionAsync(
        long attributeDefinitionId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        return await _db.LineItemAttributes
            .AsNoTracking()
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x => x.AttributeDefinitionId == attributeDefinitionId)
            .SelectCacheInfo(status)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
