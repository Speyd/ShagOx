using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Caches;
using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Query;
using ShagOxServer.Application.DTOs.Orders.Core.Query;
using ShagOxServer.Application.DTOs.Orders.OrderItems.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
using ShagOxServer.Application.Interfaces.Repositories.Payments.Core;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Query;
using ShagOxServer.Application.Interfaces.Services.Orders.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Orders.OrderItems.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Query;
using ShagOxServer.Application.Services.Base.Localized;
using ShagOxServer.Application.Services.Caches.Keys.Orders;
using ShagOxServer.Application.Services.Orders.Core.Mapping;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Entities.Orders.Enum;
using ShagOxServer.Domain.Entities.Payments.Enum;
using ShagOxServer.Domain.Filters.Orders.Core;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Orders.Core.Query;
public class OrderQueryService
    : BaseLocalizedQueryService<
        OrderDto,
        Order,
        OrderSearchFilter
        >,
    IOrderQueryService
{
    private readonly IOrderQueryRepository _orderQueryRepository;
    private readonly IOrderItemQueryService _orderItemService;

    private readonly IDeliveryProviderQueryService _deliveryService;
    private readonly ICurrencyQueryService _currencyService;
    private readonly IPaymentQueryRepository _paymentQueryRepository;


    protected override bool CacheBySearch => false;

    protected override bool CacheByPaged => false;


    public OrderQueryService(
        IOrderQueryRepository orderQueryRepository,
        IOrderItemQueryService orderItemService,
        IDeliveryProviderQueryService deliveryService,
        ICurrencyQueryService currencyService,
        IPaymentQueryRepository paymentQueryRepository,
        IDataCacheService cacheService,
        IOptions<CacheOptions> cacheOptions,
        ILanguageProvider language
    )
        : base(orderQueryRepository, 
            language, 
            cacheService,
            cacheOptions)
    {
        _orderQueryRepository = orderQueryRepository;
        _orderItemService = orderItemService;
        _deliveryService = deliveryService;
        _currencyService = currencyService;
        _paymentQueryRepository = paymentQueryRepository;
    }


    public override async Task<OrderDto> ApplyMapperAsync(
        Order entity)
    {
        var items = new List<OrderItemDto>();

        foreach (var item in entity.OrderItems)
        {
            var itemDto = await _orderItemService
                .ApplyMapperAsync(item);

            items.Add(itemDto);
        }

        var currency = await _currencyService
            .ApplyMapperAsync(entity.Currency);

        DeliveryProviderDto? delivery = null;

        if (entity.DeliveryProvider is not null)
        {
            delivery = await _deliveryService
                .ApplyMapperAsync(entity.DeliveryProvider);
        }     

        var paymentExpiresAt = await GetActivePaymentExpiresAtAsync(entity);

        return OrderMapper.ToDto(entity, 
            items, 
            currency, 
            delivery,
            paymentExpiresAt);
    }

    private async Task<DateTime?> GetActivePaymentExpiresAtAsync(
        Order order)
    {
        var awaitsPayment =
            order.PaymentMethod == PaymentMethod.Online &&
            order.OrderStatus == OrderStatus.Pending &&
            order.PaymentStatus == OrderPaymentStatus.Pending;

        if (!awaitsPayment)
            return null;

        var payment = await _paymentQueryRepository
            .GetActiveByOrderIdAsync(order.Id, DateTime.UtcNow);

        return payment?.ExpiresAt;
    }

    public async Task<Result<OrderDto>> GetByOrderNumberAsync(
        string orderNumber,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = OrderCache.ByOrderNumber(
            orderNumber,
            _language.Language,
            status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var order = await _orderQueryRepository
                    .GetByOrderNumberAsync(orderNumber,
                        cancellationToken, status);

                return await order.ToResultAsync(
                    ApplyMapperAsync);
            },
            _cacheOptions.KeyExpiration
        );
    }

    public async Task<Result<OrderDto>> GetByPaymentTransactionIdAsync(
        string paymentTransactionId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = OrderCache.ByPaymentTransaction(
            paymentTransactionId,
            _language.Language,
            status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var order = await 
                    _orderQueryRepository.GetByPaymentTransactionIdAsync(
                        paymentTransactionId,
                        cancellationToken, 
                        status);

                return await order.ToResultAsync(
                    ApplyMapperAsync);
            },
            _cacheOptions.KeyExpiration
        );
    }

    public async Task<Result<OrderDto>> GetByDeliveryTrackingNumberAsync(
        string trackingNumber,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = OrderCache.ByTrackingNumber(
            trackingNumber,
            _language.Language,
            status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var order = await 
                    _orderQueryRepository.GetByDeliveryTrackingNumberAsync(
                        trackingNumber,
                        cancellationToken, 
                        status);

                return await order.ToResultAsync(
                    ApplyMapperAsync);
            },
            _cacheOptions.KeyExpiration
        );
    }
}
