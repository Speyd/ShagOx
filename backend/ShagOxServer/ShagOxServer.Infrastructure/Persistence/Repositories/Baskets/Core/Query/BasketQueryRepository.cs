using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.Core;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketAttributes.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.Core.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.Core.Query;
public partial class BasketQueryRepository
    : SearchRepository<Basket, BasketSearchFilter>,
      IBasketQueryRepository
{
    public BasketQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<Basket?> GetByUserAsync(
        long userId,
        CancellationToken cancellationToken = default)
    {
        return await _db.Baskets
            .WithIncludes()
            .FirstOrDefaultAsync(c =>
                c.UserId == userId,
                cancellationToken);
    }

    public async Task<long?> GetIdByUserAsync(
        long userId,
        CancellationToken cancellationToken = default)
    {
        var basketId =  await _db.Baskets
            .FirstOrDefaultAsync(c =>
                c.UserId == userId,
                cancellationToken);

        return basketId?.Id;
    }

    public async Task<long?> GetUserIdByBasketAsync(
        long basketId,
        CancellationToken cancellationToken = default)
    {
        var basket = await _db.Baskets
            .FirstOrDefaultAsync(c => 
                c.Id == basketId,
                cancellationToken);

        return basket?.UserId;
    }
}
