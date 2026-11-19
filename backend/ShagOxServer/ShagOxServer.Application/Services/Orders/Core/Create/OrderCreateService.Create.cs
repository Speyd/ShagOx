using ShagOxServer.Application.DTOs.Orders.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Orders.Core.Create;
using ShagOxServer.Domain.Entities.Deliveries;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Entities.Orders.Enum;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Orders.Core.Create;
public partial class OrderCreateService
    : IOrderCreateService
{
    private async Task<Order> CreateOrderEntityAsync(
        long ownerId,
        DeliveryProvider? deliveryProvider,
        Currency currency,
        OrderCreateRequest request)
    {
        var orderNumber = _orderNumberGenerator.Generate();

        var order = OrderCreater.Create(ownerId, orderNumber, request);

        _orderRepository.Add(order);

        order.DeliveryProvider = deliveryProvider;
        order.Currency = currency;
        order.OrderStatus = OrderStatus.Pending;

        await _unitOfWork.SaveChangesAsync();

        return order;
    }

    private async Task<Result<bool>> CreateOrderItemsAsync(
        Order order,
        OrderCreateRequest request)
    {
        order.Subtotal = 0;
        order.TotalWeight = 0;

        foreach (var item in request.OrderItems)
        {
            var itemResult = await _orderItemCreateService
                .CreateInternalAsync(item with
                {
                    ParentId = order.Id
                });

            if (!itemResult.IsSuccess)
            {
                return Result<bool>
                    .Fail(itemResult.Error);
            }

            order.Subtotal += itemResult.Value!.TotalPrice;
            order.TotalWeight += itemResult.Value.TotalWeight;
        }

        return Result<bool>.Success(true);
    }
}
