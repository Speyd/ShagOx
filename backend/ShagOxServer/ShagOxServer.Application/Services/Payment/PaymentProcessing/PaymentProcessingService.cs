using Microsoft.Extensions.Logging;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems.Query;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
using ShagOxServer.Application.Interfaces.Repositories.Payments.Core;
using ShagOxServer.Application.Interfaces.Repositories.Payments.PaymentWebhooks;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Delete;
using ShagOxServer.Application.Interfaces.Services.Payments;
using ShagOxServer.Application.Services.Caches.Invalidations.Orders;
using ShagOxServer.Application.Services.Orders.Core.Mapping;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Application.Services.Payment.PaymentProcessing;
public partial class PaymentProcessingService
    : IPaymentProcessingService
{
    private readonly IBasketQueryRepository _basketRepository;

    private readonly IBasketItemQueryRepository _itemRepository;
    private readonly IBasketItemDeleteService _itemDeleteService;

    private readonly IPaymentQueryRepository _paymentRepository;
    private readonly IPaymentCommandRepository _paymentCommandRepository;

    private readonly IOrderCommandRepository _orderCommandRepository;

    private readonly IPaymentWebhookCommandRepository _webhookCommandRepository;
    private readonly IPaymentWebhookQueryRepository _webhookQueryRepository;

    private readonly IAdvertisementVariantCommandRepository _variantCommand;
    private readonly IAdvertisementCommandRepository _advertCommand;

    private readonly OrderInvalidationService _orderInvalidation;

    private readonly IPaymentGateway _paymentGateway;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PaymentProcessingService> _logger;

    public PaymentProcessingService(
        IBasketQueryRepository basketRepository,
        IBasketItemQueryRepository itemRepository,
        IBasketItemDeleteService itemDeleteService,
        IOrderCommandRepository orderCommandRepository,
        IPaymentCommandRepository paymentCommandRepository,
        IPaymentQueryRepository paymentRepository,
        IPaymentWebhookCommandRepository webhookCommandRepository,
        IPaymentWebhookQueryRepository webhookQueryRepository,
        IAdvertisementVariantCommandRepository variantCommand,
        IAdvertisementCommandRepository advertCommand,
        OrderInvalidationService orderInvalidation,
        IPaymentGateway paymentGateway,
        IUnitOfWork unitOfWork,
        ILogger<PaymentProcessingService> logger)
    {
        _basketRepository = basketRepository;
        _itemRepository = itemRepository;
        _itemDeleteService = itemDeleteService;
        _orderCommandRepository = orderCommandRepository;
        _paymentCommandRepository = paymentCommandRepository;
        _paymentRepository = paymentRepository;
        _webhookCommandRepository = webhookCommandRepository;
        _webhookQueryRepository = webhookQueryRepository;
        _variantCommand = variantCommand;
        _advertCommand = advertCommand;
        _orderInvalidation = orderInvalidation;
        _paymentGateway = paymentGateway;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    private async Task InvalidateOrderCacheAsync(Order order)
    {
        await _orderInvalidation.InvalidateUpdateAsync(
            OrderCacheMapper.ToInfo(order));
    }
}
