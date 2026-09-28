using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.Core;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketAttributes.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.Core.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.Core;
public class BasketQueryRepository
    : QueryRepository<Basket, BasketSearchFilter>,
      IBasketQueryRepository
{
    public BasketQueryRepository(AppDbContext db)
        : base(db)
    { }


    protected override IQueryable<Basket> ApplyIncludes(
        IQueryable<Basket> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<Basket> ApplyFilter(
        IQueryable<Basket> query,
        BasketSearchFilter filter)
    {
        return query.Filter(filter);
    }
    
    public async Task<Basket?> GetByUserAsync(
        long userId)
    {
        return await _db.Baskets
            .WithIncludes()
            .FirstOrDefaultAsync(c =>
                c.UserId == userId);
    }
}