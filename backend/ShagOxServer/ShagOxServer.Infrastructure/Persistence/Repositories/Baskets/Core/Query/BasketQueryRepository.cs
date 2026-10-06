using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.Core;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketAttributes.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.Core.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.Core.Query;
public partial class BasketQueryRepository
    : QueryRepository<Basket, BasketSearchFilter>,
      IBasketQueryRepository
{
    public BasketQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<Basket?> GetByUserAsync(
        long userId)
    {
        return await _db.Baskets
            .WithIncludes()
            .FirstOrDefaultAsync(c =>
                c.UserId == userId);
    }

    public async Task<long?> GetIdByUserAsync(
        long userId)
    {
        var basketId =  await _db.Baskets
            .FirstOrDefaultAsync(c =>
                c.UserId == userId);

        return basketId?.Id;
    }

    public async Task<long?> GetUserIdByBasketAsync(
        long basketId)
    {
        var basket = await _db.Baskets
            .FirstOrDefaultAsync(c => c.Id == basketId);

        return basket?.UserId;
    }
}