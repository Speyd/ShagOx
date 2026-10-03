using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Cache;
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
    : QueryRepository<BasketAttribute, BasketAttributeSearchFilter>,
      IBasketAttributeQueryRepository
{
    public BasketAttributeQueryRepository(AppDbContext db)
        : base(db)
    { }


    protected override IQueryable<BasketAttribute> ApplyIncludes(
        IQueryable<BasketAttribute> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<BasketAttribute> ApplyFilter(
      IQueryable<BasketAttribute> query,
      BasketAttributeSearchFilter filter)
    {
        return query.Filter(filter);
    }

    public async Task<PagedResult<BasketAttribute>> GetByCategoryAsync(
        long categoryId,
        PaginationParams pagination)
    {
        return await _db.BasketAttributes
            .WithIncludes()
            .Where(c =>
                c.AttributeDefinition.CategoryId == categoryId)
            .OrderBy(c => c.Order)
            .ToPagedResultAsync(pagination);
    }

    public async Task<List<BasketAttributeCacheInfo>> GetCacheInfoByCategoryAsync(
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

    public async Task<BasketAttribute?> GetByAttributeDefinitionAsync(
        long attributeDefinitionId)
    {
        return await _db.BasketAttributes
            .WithIncludes()
            .FirstOrDefaultAsync(c =>
                c.AttributeDefinitionId == attributeDefinitionId);
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