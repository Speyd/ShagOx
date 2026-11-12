using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.LineItems.Core;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems;
public class BasketItemExistsRepository
    : LineItemExistsRepository<BasketItem>,
      IBasketItemExistsRepository
{
    public BasketItemExistsRepository(
        AppDbContext db)
        : base(db)
    { }


    public override async Task<bool> IsOwnerAsync(
        long enityId,
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        var result = await _db.BasketItems
            .WithStatusFilter(status)
            .WithIncludes()
            .AnyAsync(x =>
                x.Id == enityId &&
                x.Parent.OwnerId == userId);

        return result;
    }
  
}
