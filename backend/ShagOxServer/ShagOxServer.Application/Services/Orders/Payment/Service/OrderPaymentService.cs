using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Orders;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Providers.Payment;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core;
using ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
using ShagOxServer.Application.Interfaces.Repositories.Payments.Core;
using ShagOxServer.Application.Interfaces.Services.Orders;
using ShagOxServer.Application.Interfaces.Services.Payments;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Resources.Payment;
using ShagOxServer.Domain.Entities.Orders.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Orders.Payment;

public partial class OrderPaymentService : IOrderPaymentService
{
    private readonly IOrderCommandsRepository _orderCommandRepository;
    private readonly IOrderQueryRepository _orderQueryRepository;

    private readonly IPaymentQueryRepository _paymentQueryRepository;

    private readonly IPaymentProcessingService _paymentProcessing;
    private readonly IPaymentProvider _paymentProvider;

    private readonly IAdvertisementCommandRepository _advertCommand;
    private readonly IAdvertisementVariantCommandsRepository _variantCommand;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrderPaymentService> _logger;

    public OrderPaymentService(
        IOrderCommandsRepository orderCommandRepository,
        IOrderQueryRepository orderQueryRepository,
        IPaymentQueryRepository paymentQueryRepository,
        IPaymentProcessingService paymentProcessing,
        IPaymentProvider paymentProvider,
        IAdvertisementCommandRepository advertCommand,
        IAdvertisementVariantCommandsRepository variantCommand,
        IUnitOfWork unitOfWork,
        ILogger<OrderPaymentService> logger)
    {
        _orderCommandRepository = orderCommandRepository;
        _orderQueryRepository = orderQueryRepository;
        _paymentQueryRepository = paymentQueryRepository;
        _paymentProcessing = paymentProcessing;
        _paymentProvider = paymentProvider;
        _advertCommand = advertCommand;
        _variantCommand = variantCommand;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<OrderPaymentResponse>> GetOrCreatePaymentAsync(
        long orderId,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        var order = await _orderQueryRepository
            .GetByIdIncludeAsync(orderId, cancellationToken);

        if (order is null)
        {
            return Result<OrderPaymentResponse>.NotFound(
                EntityNamesResources.Order);
        }

        var stateError = ValidatePayable(order, now);

        if (stateError is not null)
            return Result<OrderPaymentResponse>.Fail(stateError);

        var needsReopen = order.OrderStatus == OrderStatus.Cancelled;

        if (!needsReopen)
        {
            var active = await _paymentQueryRepository
                .GetActiveByOrderIdAsync(orderId, now, cancellationToken);

            if (active?.PaymentUrl is not null)
            {
                return Result<OrderPaymentResponse>.Success(
                    new OrderPaymentResponse(
                        active.PaymentUrl,
                        active.ExpiresAt));
            }

            var open = await _paymentQueryRepository
                .GetOpenByOrderIdAsync(orderId, cancellationToken);

            if (open.Any(p => !OrderPaymentPolicy.IsExpired(p, now)))
            {
                return Result<OrderPaymentResponse>.Fail(
                    PaymentResources.OrderPaymentInProgress);
            }

            foreach (var payment in open)
            {
                var failed = await _paymentProcessing
                    .ProcessFailedPayment(payment, cancellationToken);

                if (!failed.IsSuccess)
                {
                    return Result<OrderPaymentResponse>
                        .Fail(failed.Error);
                }
            }

            needsReopen = open.Count > 0;
        }

        return await CreatePaymentAsync(
            order,
            needsReopen,
            cancellationToken);
    }
}
