using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.Core;
public class BasketExistsRepository
    : ExistsOwnerRepository<Basket>,
      IBasketExistsRepository
{
    public BasketExistsRepository(
        AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsByUserAsync(
        long userId,
        CancellationToken cancellationToken = default)
    {
        return await _db.Baskets
           .AnyAsync(c => c.OwnerId == userId,
            cancellationToken);
    }
}
