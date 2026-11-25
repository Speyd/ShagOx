using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Delete;
using ShagOxServer.Application.Interfaces.Services.Baskets.Core.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Base.Delete;
using ShagOxServer.Application.Services.Baskets.Core.Validator;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Baskets.Core.Delete;
public class BasketDeleteService
    : BaseDeleteService<Basket>
    , IBasketDeleteService
{
    private readonly IRepository<Basket> _basketRepository;
    private readonly BasketValidator _basketValidator;

    private readonly IBasketItemDeleteService _itemDeleteService;


    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<BasketDeleteService> _logger;


    public BasketDeleteService(
        IRepository<Basket> basketRepository,
        ICommandRepository<Basket> basketCommand,
        BasketValidator basketValidator,
        IBasketItemDeleteService itemDeleteService,
        IUnitOfWork unitOfWork,
        ILogger<BasketDeleteService> logger
    )
        : base(basketCommand, basketValidator, unitOfWork, logger)
    {
        _basketRepository = basketRepository;
        _basketValidator = basketValidator;
        _itemDeleteService = itemDeleteService;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        var basket = await _basketValidator
            .GetByIdAsync(id, cancellationToken);

        if (!basket.IsSuccess)
            return Result<DeleteResponse>.Fail(basket.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _basketRepository.Delete(basket.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to delete basket. Id: {Id}",
                id);

            return Result<DeleteResponse>
                .Fail(EntityErrorResources.BasketDeleteFailed);
        }

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               basket.Value!.Id,
               DateTime.UtcNow
           )
       );
    }

    public override async Task<Result<bool>> SoftBodyDeleteAsync(
        Basket entity,
        CancellationToken cancellationToken = default)
    {
        var body = await base.SoftBodyDeleteAsync(entity,
            cancellationToken);

        if (!body.IsSuccess)
            Result<bool>.Fail(body.Error);

        foreach (var image in entity.BasketItems)
        {
            var imageDelete = await _itemDeleteService
                .SoftDeleteRecordAsync(image, cancellationToken);

            if (!imageDelete.IsSuccess)
                Result<bool>.Fail(imageDelete.Error);
        }

        return Result<bool>.Success(true);
    }
}
