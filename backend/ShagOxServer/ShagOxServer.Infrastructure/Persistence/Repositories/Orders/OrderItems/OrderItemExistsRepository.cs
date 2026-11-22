using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Orders.OrderItems;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.LineItems.Core;
using ShagOxServer.Infrastructure.Persistence.Repositories.Orders.OrderItems.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Orders.OrderItems;
public class OrderItemExistsRepository
    : LineItemExistsRepository<OrderItem>,
      IOrderItemExistsRepository
{
    public OrderItemExistsRepository(
        AppDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }


    public override async Task<bool> IsOwnerAsync(
        long enityId,
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        var result = await _db.OrderItems
            .WithStatusFilter(status)
            .WithIncludes()
            .AnyAsync(x =>
                x.Id == enityId &&
                x.Parent.OwnerId == userId);

        return result;
    }

}
