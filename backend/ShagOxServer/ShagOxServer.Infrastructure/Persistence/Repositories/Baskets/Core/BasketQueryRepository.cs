using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.Core;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.Core.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.Core;
public class BasketQueryRepository
    : QueryRepository<Basket>,
      IBasketQueryRepository
{
    public BasketQueryRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<PagedResult<Basket>> GetByUserAsync(
        int userId,
        PaginationParams pagination)
    {
        return await _db.Baskets
            .Where(c =>
                c.UserId == userId)
            .ToPagedResultAsync(pagination);
    }

    public async Task<PagedResult<Basket>> Search(
        BasketSearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.Baskets
           .Filter(filter)
           .ToPagedResultAsync(pagination);
    }
}