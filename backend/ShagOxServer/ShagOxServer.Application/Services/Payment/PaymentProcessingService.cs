using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems.Query;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Delete;
using ShagOxServer.Application.Interfaces.Services.Payments;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Payments;
public class PaymentProcessingService
    : IPaymentProcessingService
{
    private readonly IBasketQueryRepository _basketRepository;
    private readonly IBasketItemQueryRepository _itemRepository;
    private readonly IBasketItemDeleteService _itemDeleteService;
    private readonly IOrderQueryRepository _orderRepository;
    private readonly IOrderCommandRepository _orderCommandRepository;
    private readonly IUnitOfWork _unitOfWork;


    public PaymentProcessingService(
        IBasketQueryRepository basketRepository,
        IBasketItemQueryRepository itemRepository,
        IBasketItemDeleteService itemDeleteService,
        IOrderCommandRepository orderCommandRepository,
        IOrderQueryRepository orderRepository,
        IUnitOfWork unitOfWork)
    {
        _basketRepository = basketRepository;
        _itemRepository = itemRepository;
        _itemDeleteService = itemDeleteService;
        _orderCommandRepository = orderCommandRepository;
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<bool>> ProcessSuccessfulPayment(
        string invoiceId,
        CancellationToken cancellationToken = default)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var order = await _orderRepository.GetByInvoiceIdAsync(
                invoiceId,
                cancellationToken);

            if (order is null)
            {
                await _unitOfWork.RollbackAsync();

                return Result<bool>.Fail(
                    EntityNamesResources.Order);
            }
            var isPaid = await _orderCommandRepository.TryMarkAsPaidAsync(
                invoiceId,
                order.PaymentStatus,
                cancellationToken);

            if (!isPaid)
            {
                await _unitOfWork.RollbackAsync();

                return Result<bool>.Success(true);
            }

            var deleteResult = await DeleteBasketItems(order);

            if (!deleteResult.IsSuccess)
            {
                await _unitOfWork.RollbackAsync();

                return Result<bool>
                    .Fail(deleteResult.Error);
            }

            await _unitOfWork.CommitAsync();

            return Result<bool>.Success(true);
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    private async Task<Result<bool>> DeleteBasketItems(
        Order order,
        CancellationToken cancellationToken = default)
    {
        var basketId = await _basketRepository
            .GetIdByUserAsync(order.OwnerId);

        if(!basketId.HasValue)
        {
            return Result<bool>.Fail(
                EntityNamesResources.Basket);
        }

        var ids = order.OrderItems
            .Select(x =>
                (x.AdvertisementId, x.AdvertisementVariantId))
            .ToList();

        if (order.OrderItems is null || !order.OrderItems.Any())
        {
            return Result<bool>
                .Fail(EntityNamesResources.OrderItem);
        }


        var items = await _itemRepository
            .GetByParentAndDependenciesAsync(
                basketId.Value,
                ids,
                cancellationToken);

        foreach(var item in items)
        {
            var deleteResult = await _itemDeleteService
                .SoftDeleteRecordAsync(item, cancellationToken);

            if (!deleteResult.IsSuccess)
            {
                return Result<bool>
                    .Fail(deleteResult.Error);
            }
        }

        return Result<bool>.Success(true);
    }
}
