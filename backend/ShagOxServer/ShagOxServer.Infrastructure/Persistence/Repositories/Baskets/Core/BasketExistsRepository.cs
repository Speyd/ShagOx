using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.Core;
public class BasketExistsRepository
    : ExistsRepository<Basket>,
      IBasketExistsRepository
{
    public BasketExistsRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsByUserAsync(
        int userId)
    {
        return await _db.Baskets
           .AnyAsync(c => c.UserId == userId);
    }

    public async Task<bool> IsOwnerAsync(
        int basketId, 
        int userId)
    {
        return await _db.Baskets
            .AnyAsync(c =>
                c.UserId == userId &&
                c.Id == basketId
            );

    }
}