using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Baskets.BasketItems.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Create;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Application.Services.Baskets.BasketItems.Validator;
using ShagOxServer.Application.Services.Baskets.Core.Validator;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Create;
public class BasketItemCreateService
    : IBasketItemCreateService
{
    private readonly IRepository<BasketItem> _itemRepository;
    private readonly BasketItemValidator _itemValidator;

    private readonly BasketValidator _basketValidator;
    private readonly AdvertisementValidator _advertValidator;



    private readonly IUnitOfWork _unitOfWork;


    public BasketItemCreateService(
        IRepository<BasketItem> itemRepository,
        BasketItemValidator itemValidator,
        BasketValidator basketValidator,
        AdvertisementValidator advertValidator,
        IUnitOfWork unitOfWork)
    {
        _itemRepository = itemRepository;
        _itemValidator = itemValidator;
        _basketValidator = basketValidator;
        _advertValidator = advertValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        BasketItemCreateRequest request)
    {
        var basketExists = await _basketValidator
            .ExistsByIdAsync(request.BasketId);
        if (!basketExists.IsSuccess)
            return Result<CreateResponse>.Fail(basketExists.Error);

        var advertExists = await _advertValidator
            .ExistsByIdAsync(request.AdvertisementId);
        if (!advertExists.IsSuccess)
            return Result<CreateResponse>.Fail(advertExists.Error);

        var itemExists = await _itemValidator
           .NotExistsAsync(request.AdvertisementId, request.BasketId);
        if (!itemExists.IsSuccess)
            return Result<CreateResponse>.Fail(itemExists.Error);

        var item = BasketItemCreater.Create(request);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _itemRepository.Add(item);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<CreateResponse>.Success(
            new CreateResponse(
                item.Id,
                DateTime.UtcNow
        ));
    }
}