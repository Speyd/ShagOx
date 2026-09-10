using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketAttributes;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketAttributes.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketAttributes;
public class BasketAttributeQueryRepository
    : QueryRepository<BasketAttribute>,
      IBasketAttributeQueryRepository
{
    public BasketAttributeQueryRepository(AppDbContext db)
        : base(db)
    { }


    public override async Task<BasketAttribute?> GetByIdAsync(
        int id)
    {
        return await _db.BasketAttributes
            .WithIncludes()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<PagedResult<BasketAttribute>> GetByCategoryAsync(
        int categoryId,
        PaginationParams pagination)
    {
        return await _db.BasketAttributes
            .WithIncludes()
            .Where(c =>
                c.AttributeDefinition.CategoryId == categoryId)
            .OrderBy(c => c.Order)
            .ToPagedResultAsync(pagination);
    }

    public async Task<BasketAttribute?> GetByAttributeDefenitionAsync(
        int attributeDefenitionId)
    {
        return await _db.BasketAttributes
            .WithIncludes()
            .FirstOrDefaultAsync(c =>
                c.AttributeDefinitionId == attributeDefenitionId);
    }

    public override async Task<PagedResult<BasketAttribute>> GetPagedAsync(
        PaginationParams pagination)
    {
        return await _db.BasketAttributes
            .WithIncludes()
            .ToPagedResultAsync(pagination);
    }

    public async Task<PagedResult<BasketAttribute>> Search(
        BasketAttributeSearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.BasketAttributes
           .WithIncludes()
           .Filter(filter)
           .ToPagedResultAsync(pagination);
    }
}