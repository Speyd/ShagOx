using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Orders.OrderItems.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Base.Delete;
using ShagOxServer.Application.Services.Caches.Invalidations.Orders.OrderItems;
using ShagOxServer.Application.Services.LineItems.Core.Mapping;
using ShagOxServer.Application.Services.Orders.OrderItems.Validator;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Orders.OrderItems.Delete;
public class OrderItemDeleteService
    : BaseDeleteService<OrderItem>
    , IOrderItemDeleteService
{
    private readonly IRepository<OrderItem> _itemRepository;
    private readonly OrderItemValidator _itemValidator;

    private readonly OrderItemInvalidationService _itemInvalidation;
    private readonly OrderItemOrderInvalidationService _orderInvalidation;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrderItemDeleteService> _logger;


    public OrderItemDeleteService(
        IRepository<OrderItem> itemRepository,
        OrderItemValidator itemValidator,
        OrderItemInvalidationService itemInvalidation,
        OrderItemOrderInvalidationService orderValidator,
        IUnitOfWork unitOfWork,
        ILogger<OrderItemDeleteService> logger
    )
        : base(itemValidator, unitOfWork, logger)
    {
        _itemRepository = itemRepository;
        _itemValidator = itemValidator;
        _itemInvalidation = itemInvalidation;
        _orderInvalidation = orderValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        var item = await _itemValidator
            .GetByIdAsync(id, cancellationToken);

        if (!item.IsSuccess)
            return Result<DeleteResponse>.Fail(item.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _itemRepository.Delete(item.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to delete order item. Id: {Id}",
                id);

            return Result<DeleteResponse>.Fail(
                EntityErrorResources.OrderItemDeleteFailed);
        }

        _logger.LogInformation(
            "Order item deleted successfully. BasketItemId: {BasketItemId}",
            item.Value!.Id);

        await InvalidateCache(item.Value!);

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               item.Value!.Id,
               DateTime.UtcNow
           )
       );
    }

    public override async Task InvalidateCache(
        OrderItem entity)
    {
        var info = LineItemCacheMapper
            .ToInfo(entity);

        await _itemInvalidation
            .InvalidateDeleteAsync(info);

        await _orderInvalidation
            .InvalidateDeleteAsync(info);
    }
}
