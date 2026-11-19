using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Application.Services.Orders.Core.Validator;
public class OrderValidator
    : BaseValidator<Order>
{
    public OrderValidator(
        IRepository<Order> repository,
        IOrderExistsRepository orderExistsRepository,
        IOrderQueryRepository orderQeuryRepository,
        IDataCacheService cache
    )
        : base(repository,
            orderQeuryRepository,
            orderExistsRepository,
            cache)
    {
    }
}
