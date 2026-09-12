using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.Core;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketAttributes.Extensions;
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

    public override async Task<Basket?> GetByIdAsync(
       int id)
    {
        return await _db.Baskets
            .WithIncludes()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Basket?> GetByUserAsync(
        int userId)
    {
        return await _db.Baskets
            .WithIncludes()
            .FirstOrDefaultAsync(c =>
                c.UserId == userId);
    }

    public override async Task<PagedResult<Basket>> GetPagedAsync(
        PaginationParams pagination)
    {
        return await _db.Baskets
            .WithIncludes()
            .ToPagedResultAsync(pagination);
    }

    public async Task<PagedResult<Basket>> Search(
        BasketSearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.Baskets
            .WithIncludes()
           .Filter(filter)
           .ToPagedResultAsync(pagination);
    }
}