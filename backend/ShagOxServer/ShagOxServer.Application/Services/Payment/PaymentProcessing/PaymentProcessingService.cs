using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems.Query;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
using ShagOxServer.Application.Interfaces.Repositories.Payments.Core;
using ShagOxServer.Application.Interfaces.Repositories.Payments.PaymentWebhooks;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Delete;
using ShagOxServer.Application.Interfaces.Services.Payments;

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

    private readonly IUnitOfWork _unitOfWork;

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
        IUnitOfWork unitOfWork)
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
        _unitOfWork = unitOfWork;
    }
}
