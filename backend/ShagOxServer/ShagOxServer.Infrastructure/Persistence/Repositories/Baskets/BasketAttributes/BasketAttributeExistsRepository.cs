using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketAttributes.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketAttributes;
public class BasketAttributeExistsRepository
    : ExistsRepository<BasketAttribute>,
      IBasketAttributeExistsRepository
{
    public BasketAttributeExistsRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsAsync(
        long categoryId,
        long attributeId,
        int order)
    {
        return await _db.BasketAttributes
            .WithIncludes()
            .AnyAsync(c =>
                (c.AttributeDefinitionId == attributeId && c.Order == order) ||
                (c.AttributeDefinition.CategoryId == categoryId &&
                c.Order == order));       
    }

    public async Task<bool> ExistsByAttributeDefenitionAsync(
        long attributeDefenitionId)
    {
        return await _db.BasketAttributes
            .WithIncludes()
            .AnyAsync(c =>
                c.AttributeDefinitionId == attributeDefenitionId);
    }

    public async Task<bool> ExistsByCategoryAsync(
        long categoryId)
    {
        return await _db.BasketAttributes
            .WithIncludes()
            .AnyAsync(c =>
                 c.AttributeDefinition.CategoryId == categoryId);
    }
}