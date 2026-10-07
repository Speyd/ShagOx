using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.Core;
public class BasketExistsRepository
    : ExistsRepository<Basket>,
      IBasketExistsRepository
{
    public BasketExistsRepository(
        AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsByUserAsync(
        long userId)
    {
        return await _db.Baskets
           .AnyAsync(c => c.UserId == userId);
    }

    public async Task<bool> IsOwnerAsync(
        long basketId,
        long userId)
    {
        return await _db.Baskets
            .AnyAsync(c =>
                c.UserId == userId &&
                c.Id == basketId
            );

    }
}