using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Orders.OrderItems;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.LineItems.Core.Validator;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Application.Services.Orders.OrderItems.Validator;
public class OrderItemValidator
    : LineItemValidator<OrderItem>
{
    public OrderItemValidator(
        IRepository<OrderItem> repository,
        IOrderItemExistsRepository itemExistsRepository,
        IOrderItemQueryRepository itemQeuryRepository,
        IDataCacheService cache
    )
        : base(repository,
            itemExistsRepository,
            itemQeuryRepository,
            cache)
    {
    }
}
