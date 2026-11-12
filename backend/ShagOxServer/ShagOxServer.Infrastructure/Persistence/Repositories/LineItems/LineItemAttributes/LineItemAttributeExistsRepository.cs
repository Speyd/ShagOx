using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.LineItems.LineItemAttributes;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.LineItems;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.LineItems.LineItemAttributes.Extensions;
using System.Text.Json;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.LineItems.LineItemAttributes;
public class LineItemAttributeExistsRepository
    : ExistsRepository<LineItemAttribute>,
      ILineItemAttributeExistsRepository
{
    public LineItemAttributeExistsRepository(
        AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsAsync(
        long categoryId,
        long attributeId,
        int order,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.LineItemAttributes
            .WithIncludes()
            .WithStatusFilter(status)
            .AnyAsync(c =>
                (c.AttributeDefinitionId == attributeId && c.Order == order) ||
                (c.AttributeDefinition.CategoryId == categoryId &&
                c.Order == order),
                cancellationToken);       
    }

    public async Task<bool> ExistsByAttributeDefenitionAsync(
        long attributeDefenitionId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.LineItemAttributes
            .WithIncludes()
            .WithStatusFilter(status)
            .AnyAsync(c =>
                c.AttributeDefinitionId == attributeDefenitionId,
                cancellationToken);
    }

    public async Task<bool> ExistsByCategoryAsync(
        long categoryId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.LineItemAttributes
            .WithIncludes()
            .WithStatusFilter(status)
            .AnyAsync(c =>
                 c.AttributeDefinition.CategoryId == categoryId,
                 cancellationToken);
    }

    public async Task<List<string>> ExistsByKeysAsync(
        JsonDocument attributes,
        long categoryId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        var keys = attributes.RootElement
            .EnumerateObject()
            .Select(x => x.Name)
            .ToList();

        return await _db.LineItemAttributes
            .WithStatusFilter(status)
            .Where(x => x.AttributeDefinition.CategoryId == categoryId
                 && keys.Contains(x.AttributeDefinition.Key))
            .Select(x => x.AttributeDefinition.Key)
            .Distinct()
            .ToListAsync(cancellationToken);
    }
}
