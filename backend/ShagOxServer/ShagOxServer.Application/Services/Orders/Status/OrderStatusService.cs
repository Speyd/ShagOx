using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Orders;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Providers.Payment;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core;
using ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
using ShagOxServer.Application.Interfaces.Repositories.Payments.Core;
using ShagOxServer.Application.Interfaces.Services.Deliveries.Queries;
using ShagOxServer.Application.Interfaces.Services.Orders;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Resources.Orders.Order;
using ShagOxServer.Domain.Entities.Deliveries.Enum;
using ShagOxServer.Domain.Entities.Orders.Enum;
using ShagOxServer.Domain.Entities.Payments.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Orders.Status;

public partial class OrderStatusService
    : IOrderStatusService
{
    private static readonly OrderStatus[] BuyerCancellable =
        [OrderStatus.Pending, OrderStatus.Confirmed];

    private static readonly OrderStatus[] SellerRejectable =
        [OrderStatus.Pending, OrderStatus.Confirmed];

    private static readonly OrderStatus[] Completable =
    [
        OrderStatus.ReadyForShipment,
        OrderStatus.Shipped,
        OrderStatus.InTransit,
        OrderStatus.Delivered
    ];

    private readonly IOrderQueryRepository _orderCommand;
    private readonly IOrderCommandsRepository _orderCommands;

    private readonly IPaymentQueryRepository _paymentQuery;
    private readonly IPaymentCommandsRepository _paymentCommand;

    private readonly IAdvertisementCommandRepository _advertCommand;
    private readonly IAdvertisementVariantCommandsRepository _variantCommand;

    private readonly IPaymentGateway _paymentGateway;
    private readonly IDeliveryQueriesService _deliveryQueries;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrderStatusService> _logger;


    public OrderStatusService(
        IOrderQueryRepository orderCommand,
        IOrderCommandsRepository orderCommands,
        IPaymentQueryRepository paymentQuery,
        IPaymentCommandsRepository paymentCommand,
        IPaymentGateway paymentGateway,
        IAdvertisementCommandRepository advertCommand,
        IAdvertisementVariantCommandsRepository variantCommand,
        IDeliveryQueriesService deliveryQueries,
        IUnitOfWork unitOfWork,
        ILogger<OrderStatusService> logger)
    {
        _orderCommand = orderCommand;
        _orderCommands = orderCommands;

        _paymentQuery = paymentQuery;
        _paymentCommand = paymentCommand;

        _paymentGateway = paymentGateway;

        _advertCommand = advertCommand;
        _variantCommand = variantCommand;

        _deliveryQueries = deliveryQueries;

        _unitOfWork = unitOfWork;
        _logger = logger;
    }



    public async Task<Result<OrderStatusResponse>> CancelByBuyerAsync(
        long orderId,
        long buyerId,
        CancellationToken cancellationToken = default)
    {
        var order = await _orderCommand
            .GetForPaymentAsync(orderId, cancellationToken);

        if (order is null || order.OwnerId != buyerId)
            return Result<OrderStatusResponse>.NotFound(EntityNamesResources.Order);

        if (!BuyerCancellable.Contains(order.OrderStatus))
            return Fail(OrderResources.OrderCancelNotAllowed);

        return await CloseAsync(
            order,
            BuyerCancellable,
            OrderStatus.Cancelled,
            cancellationToken);
    }

    public async Task<Result<OrderStatusResponse>> ConfirmBySellerAsync(
        long orderId,
        long sellerId,
        CancellationToken cancellationToken = default)
    {
        var (order, error) = await GetSellerOrderAsync(
            orderId, sellerId, cancellationToken);

        if (error is not null)
            return error;

        if (order!.OrderStatus != OrderStatus.Pending)
            return Fail(OrderResources.OrderConfirmNotAllowed);

        if (order.PaymentMethod == PaymentMethod.Online &&
            order.PaymentStatus != OrderPaymentStatus.Paid)
        {
            return Fail(OrderResources.OrderConfirmUnpaid);
        }

        var changed = await _orderCommands
            .TryConfirmAsync(order.Id, cancellationToken);

        return await FinishAsync(order, changed, OrderStatus.Confirmed, stockChanged: false);
    }

    public async Task<Result<OrderStatusResponse>> RejectBySellerAsync(
        long orderId,
        long sellerId,
        CancellationToken cancellationToken = default)
    {
        var (order, error) = await GetSellerOrderAsync(
            orderId, sellerId, cancellationToken);

        if (error is not null)
            return error;

        if (!SellerRejectable.Contains(order!.OrderStatus))
            return Fail(OrderResources.OrderRejectNotAllowed);

        return await CloseAsync(
            order,
            SellerRejectable,
            OrderStatus.Rejected,
            cancellationToken);
    }

    public async Task<Result<OrderStatusResponse>> ShipBySellerAsync(
        long orderId,
        long sellerId,
        string? trackingNumber,
        CancellationToken cancellationToken = default)
    {
        var (order, error) = await GetSellerOrderAsync(
            orderId, sellerId, cancellationToken);

        if (error is not null)
            return error;

        if (order!.OrderStatus != OrderStatus.Confirmed)
            return Fail(OrderResources.OrderShipNotAllowed);

        if (order.DeliveryType == DeliveryType.Pickup)
        {
            var ready = await _orderCommands.TryShipAsync(
                order.Id,
                OrderStatus.ReadyForShipment,
                trackingNumber: null,
                trackingUrl: null,
                cancellationToken);

            return await FinishAsync(order, ready, OrderStatus.ReadyForShipment, stockChanged: false);
        }

        var number = trackingNumber?.Trim();

        if (string.IsNullOrEmpty(number))
            return Fail(OrderResources.OrderTrackingNumberRequired);

        var trackingUrl = _deliveryQueries
            .GetTrackingUrl(order.DeliveryProvider!.Code, number);

        var shipped = await _orderCommands.TryShipAsync(
            order.Id,
            OrderStatus.Shipped,
            number,
            trackingUrl,
            cancellationToken);

        return await FinishAsync(order, shipped, OrderStatus.Shipped, stockChanged: false);
    }

    public async Task<Result<OrderStatusResponse>> CompleteAsync(
        long orderId,
        long userId,
        CancellationToken cancellationToken = default)
    {
        var order = await _orderCommand
            .GetForPaymentAsync(orderId, cancellationToken);

        if (order is null)
            return Result<OrderStatusResponse>.NotFound(EntityNamesResources.Order);

        var isBuyer = order.OwnerId == userId;

        if (!isBuyer)
        {
            var access = CheckSellerAccess(order, userId);
            if (access is not null)
                return access;
        }

        if (!Completable.Contains(order.OrderStatus))
            return Fail(OrderResources.OrderCompleteNotAllowed);

        var changed = await _orderCommands.TryCompleteAsync(
            order.Id,
            Completable,
            cancellationToken);

        return await FinishAsync(order, changed, OrderStatus.Completed, stockChanged: false);
    }
}
