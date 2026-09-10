using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketAttributes.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketAttributes;
public class BasketAttributeExistsRepository
    : ExistsRepository<BasketAttribute>,
      IBasketAttributeExistsRepository
{
    public BasketAttributeExistsRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsByAttributeDefenitionAsync(
        int attributeDefenitionId)
    {
        return await _db.BasketAttributes
            .WithIncludes()
            .AnyAsync(c =>
                c.AttributeDefinitionId == attributeDefenitionId);
    }

    public async Task<bool> ExistsByCategoryAsync(int categoryId)
    {
        return await _db.BasketAttributes
            .WithIncludes()
            .AnyAsync(c =>
                 c.AttributeDefinition.CategoryId == categoryId);
    }
}