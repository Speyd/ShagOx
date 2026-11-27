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

        order.DeliveryProviderId = deliveryProvider?.Id;
        order.CurrencyId = currency.Id;
      

        order.OrderStatus = OrderStatus.Pending;

        _orderRepository.Add(order);

        await _unitOfWork.SaveChangesAsync();

        order.Currency = currency;
        order.DeliveryProvider = deliveryProvider;

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
                    ParentId = order.Id,
                    CurrencyId = order.CurrencyId

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
