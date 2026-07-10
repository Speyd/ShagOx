using ShagOxServer.Application.DTOs.Advertisements.Update;
using ShagOxServer.Application.DTOs.Specification.Images.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Update;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Images.Create;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Images.Delete;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Update;

public class AdvertisementUpdateService : IAdvertisementUpdateService
{
    private readonly IAdvertisementRepository _advertisementRepository;

    private readonly IUserRepository _userRepository;

    private readonly ICurrencyRepository _currencyRepository;

    private readonly ICategoryRepository _categoryRepository;

    private readonly IConditionRepository _conditionRepository;

    private readonly IImageCreateService _imageCreateService;
    private readonly IImageDeleteService _imageDeleteService;

    private readonly IUnitOfWork _unitOfWork;


    public AdvertisementUpdateService(
        IAdvertisementRepository advertisementRepository,
        IUserRepository userRepository,
        ICurrencyRepository currencyRepository,
        ICategoryRepository categoryRepository,
        IConditionRepository conditionRepository,
        IImageCreateService imageCreateService,
        IImageDeleteService imageDeleteService,
        IUnitOfWork unitOfWork)
    {
        _advertisementRepository = advertisementRepository;
        _userRepository = userRepository;
        _currencyRepository = currencyRepository;
        _categoryRepository = categoryRepository;
        _conditionRepository = conditionRepository;
        _imageCreateService = imageCreateService;
        _imageDeleteService = imageDeleteService;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<AdvertisementUpdateResponse>> UpdateAdvertisementAsync(
        int advertId,
        AdvertisementUpdateRequest request)
    {
        var advert = await _advertisementRepository.GetByIdAsync(advertId);

        if (advert is null)
            return Result<AdvertisementUpdateResponse>
                .NotFound("Advertisement");


        var validation = await ValidateAsync(request);

        if (!validation.IsSuccess)
            return Result<AdvertisementUpdateResponse>
                .Fail(validation.Error!);


        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var updatedCount = UpdateAdvertisementFields(
                advert,
                request);


            var imagesResult = await UpdateImagesAsync(
                advert.Id,
                request);


            if (!imagesResult.IsSuccess)
            {
                await _unitOfWork.RollbackAsync();

                return Result<AdvertisementUpdateResponse>
                    .Fail(imagesResult.Error!);
            }


            await _unitOfWork.CommitAsync();


            return Result<AdvertisementUpdateResponse>.Success(
                new AdvertisementUpdateResponse(
                    DateTime.UtcNow,
                    updatedCount));
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }



    private async Task<Result<bool>> ValidateAsync(
        AdvertisementUpdateRequest request)
    {
        if (request.BuyerId is not null)
        {
            var buyer = await _userRepository
                .GetByIdAsync(request.BuyerId.Value);

            if (buyer is null)
                return Result<bool>.NotFound("Buyer");
        }


        if (request.CurrencyId is not null)
        {
            var currency = await _currencyRepository
                .GetByIdAsync(request.CurrencyId.Value);

            if (currency is null)
                return Result<bool>.NotFound("Currency");
        }


        if (request.ConditionId is not null)
        {
            var condition = await _conditionRepository
                .GetByIdAsync(request.ConditionId.Value);

            if (condition is null)
                return Result<bool>.NotFound("Condition");
        }


        if (request.CategoryId is not null)
        {
            var category = await _categoryRepository
                .GetByIdAsync(request.CategoryId.Value);

            if (category is null)
                return Result<bool>.NotFound("Category");
        }


        return Result<bool>.Success(true);
    }



    private int UpdateAdvertisementFields(
        Advertisement advert,
        AdvertisementUpdateRequest request)
    {
        int count = 0;


        if (request.Title is not null)
        {
            advert.Title = request.Title;
            count++;
        }


        if (request.Description is not null)
        {
            advert.Description = request.Description;
            count++;
        }


        if (request.Price is not null)
        {
            var price = request.Price.Value;

            if (price < advert.Price)
                advert.PreviousPrice = advert.Price;

            advert.Price = price;

            count++;
        }


        if (request.Popularity is not null)
        {
            advert.Popularity = request.Popularity.Value;
            count++;
        }


        if (request.CurrencyId is not null)
        {
            advert.CurrencyId = request.CurrencyId.Value;
            count++;
        }


        if (request.ConditionId is not null)
        {
            advert.ConditionId = request.ConditionId.Value;
            count++;
        }


        if (request.CategoryId is not null)
        {
            advert.CategoryId = request.CategoryId.Value;
            count++;
        }


        if (request.BuyerId is not null)
        {
            advert.BuyerId = request.BuyerId.Value;
            count++;
        }


        if (request.Properties is not null)
        {
            advert.Properties = request.Properties;
            count++;
        }


        return count;
    }



    private async Task<Result<bool>> UpdateImagesAsync(
        int advertisementId,
        AdvertisementUpdateRequest request)
    {
        if (request.DeletedImageIds is not null)
        {
            foreach (var imageId in request.DeletedImageIds)
            {
                var result = await _imageDeleteService
                    .DeleteImageAsync(imageId);


                if (!result.IsSuccess)
                    return Result<bool>.Fail(result.Error!);
            }
        }


        if (request.NewImages is not null)
        {
            foreach (var image in request.NewImages)
            {
                if (image is null)
                    continue;


                var result = await _imageCreateService
                    .CreateFromFileAsync(
                        new ImageFileCreateRequest(
                            image,
                            advertisementId));


                if (!result.IsSuccess)
                    return Result<bool>.Fail(result.Error!);
            }
        }


        return Result<bool>.Success(true);
    }
}