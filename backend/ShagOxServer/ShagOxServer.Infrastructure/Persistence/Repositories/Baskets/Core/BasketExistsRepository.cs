using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;

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
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Baskets
            .WithStatusFilter(status)
            .AnyAsync(c => c.OwnerId == userId,
                cancellationToken);
    }
}
