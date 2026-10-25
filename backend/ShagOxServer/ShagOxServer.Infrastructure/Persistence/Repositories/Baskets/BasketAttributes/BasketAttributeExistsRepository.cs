using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketAttributes.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Extensions;
using System.Text.Json;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketAttributes;
public class BasketAttributeExistsRepository
    : ExistsRepository<BasketAttribute>,
      IBasketAttributeExistsRepository
{
    public BasketAttributeExistsRepository(
        AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsAsync(
        long categoryId,
        long attributeId,
        int order,
        CancellationToken cancellationToken = default)
    {
        return await _db.BasketAttributes
            .WithIncludes()
            .AnyAsync(c =>
                (c.AttributeDefinitionId == attributeId && c.Order == order) ||
                (c.AttributeDefinition.CategoryId == categoryId &&
                c.Order == order),
                cancellationToken);       
    }

    public async Task<bool> ExistsByAttributeDefenitionAsync(
        long attributeDefenitionId,
        CancellationToken cancellationToken = default)
    {
        return await _db.BasketAttributes
            .WithIncludes()
            .AnyAsync(c =>
                c.AttributeDefinitionId == attributeDefenitionId,
                cancellationToken);
    }

    public async Task<bool> ExistsByCategoryAsync(
        long categoryId,
        CancellationToken cancellationToken = default)
    {
        return await _db.BasketAttributes
            .WithIncludes()
            .AnyAsync(c =>
                 c.AttributeDefinition.CategoryId == categoryId,
                 cancellationToken);
    }

    public async Task<List<string>> ExistsByKeysAsync(
        JsonDocument attributes,
        long categoryId,
        CancellationToken cancellationToken = default)
    {
        var keys = attributes.RootElement
            .EnumerateObject()
            .Select(x => x.Name)
            .ToList();

        return await _db.BasketAttributes
            .Where(x => x.AttributeDefinition.CategoryId == categoryId
                 && keys.Contains(x.AttributeDefinition.Key))
            .Select(x => x.AttributeDefinition.Key)
            .Distinct()
            .ToListAsync(cancellationToken);
    }
}
