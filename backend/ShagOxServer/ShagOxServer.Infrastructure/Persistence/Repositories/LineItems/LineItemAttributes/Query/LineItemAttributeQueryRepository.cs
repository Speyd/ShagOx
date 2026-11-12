using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.LineItems;
using ShagOxServer.Domain.Filters.LineItems.LineItemAttributes;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.LineItems.LineItemAttributes.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.LineItems.LineItemAttributes.Query;
public partial class LineItemAttributeQueryRepository
    : SearchRepository<LineItemAttribute, LineItemAttributeSearchFilter>,
      ILineItemAttributeQueryRepository
{
    public LineItemAttributeQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<PagedResult<LineItemAttribute>> GetByCategoryAsync(
        long categoryId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.LineItemAttributes
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(c =>
                c.AttributeDefinition.CategoryId == categoryId)
            .OrderBy(c => c.Order)
            .ToPagedResultAsync(pagination,
                (x => x.Id),
                cancellationToken);
    }

    public async Task<LineItemAttribute?> GetByAttributeDefinitionAsync(
        long attributeDefinitionId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.LineItemAttributes
            .WithIncludes()
            .WithStatusFilter(status)
            .FirstOrDefaultAsync(c =>
                c.AttributeDefinitionId == attributeDefinitionId,
                cancellationToken);

    }
}
