using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Orders.Core.Delete;
using ShagOxServer.Application.Interfaces.Services.Orders.OrderItems.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Base.Delete;
using ShagOxServer.Application.Services.Caches.Invalidations.Orders;
using ShagOxServer.Application.Services.Orders.Core.Mapping;
using ShagOxServer.Application.Services.Orders.Core.Validator;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Orders.Core.Delete;
public class OrderDeleteService
    : BaseDeleteService<Order>
    , IOrderDeleteService
{
    private readonly IRepository<Order> _orderRepository;
    private readonly OrderValidator _orderValidator;

    private readonly OrderInvalidationService _orderInvalidation;
    private readonly IOrderItemDeleteService _itemDeleteService;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrderDeleteService> _logger;


    public OrderDeleteService(
        IRepository<Order> orderRepository,
        ICommandRepository<Order> orderCommand,
        OrderValidator orderValidator,
        OrderInvalidationService orderInvalidation,
        IOrderItemDeleteService itemDeleteService,
        IUnitOfWork unitOfWork,
        ILogger<OrderDeleteService> logger
    )
        : base(orderCommand, orderValidator, unitOfWork, logger)
    {
        _orderRepository = orderRepository;
        _orderValidator = orderValidator;
        _orderInvalidation = orderInvalidation;
        _itemDeleteService = itemDeleteService;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        var order = await _orderValidator
            .GetByIdAsync(id, cancellationToken);

        if (!order.IsSuccess)
            return Result<DeleteResponse>.Fail(order.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _orderRepository.Delete(order.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to delete order. Id: {Id}",
                id);

            return Result<DeleteResponse>.Fail(
                EntityErrorResources.OrderDeleteFailed);
        }

        _logger.LogInformation(
            "Order deleted successfully. BasketItemId: {BasketItemId}",
            order.Value!.Id);

        await InvalidateCache(order.Value!);

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               order.Value!.Id,
               DateTime.UtcNow
           )
       );
    }

    public override async Task<Result<bool>> SoftBodyDeleteAsync(
        Order entity,
        CancellationToken cancellationToken = default)
    {
        var body = await base.SoftBodyDeleteAsync(entity,
            cancellationToken);

        if (!body.IsSuccess)
            return Result<bool>.Fail(body.Error);

        foreach (var image in entity.OrderItems)
        {
            var imageDelete = await _itemDeleteService
                .SoftDeleteRecordAsync(image, cancellationToken);

            if (!imageDelete.IsSuccess)
                return Result<bool>.Fail(imageDelete.Error);
        }

        return Result<bool>.Success(true);
    }

    public override async Task InvalidateCache(
        Order entity)
    {
        await _orderInvalidation.InvalidateDeleteAsync(
            OrderCacheMapper.ToInfo(entity));
    }
}
