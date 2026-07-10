using ShagOxServer.Application.DTOs.Advertisements.Update;
using ShagOxServer.Application.DTOs.Specification.Images.Create;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Images;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Update;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Images.Create;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Images.Delete;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Update;
public class AdvertisementUpdateService : IAdvertisementUpdateService
{
    private readonly IAdvertisementRepository _advertisementRepository;

    private readonly IUserRepository _userRepository;

    private readonly ICurrencyRepository _currencyRepository;

    private readonly ICategoryRepository _categoryRepository;

    private readonly IConditionRepository _conditionRepository;

    private readonly IImageQueryRepository _imageQueryRepository;
    private readonly IImageCreateService _imageCreateService;
    private readonly IImageDeleteService _imageDeleteService;


    public AdvertisementUpdateService(
        IAdvertisementRepository advertisementRepository,
        IUserRepository userRepository,
        ICurrencyRepository currencyRepository,
        ICategoryRepository categoryRepository,
        IConditionRepository conditionRepository,
        IImageQueryRepository imageQueryRepository,
        IImageCreateService imageCreateService,
        IImageDeleteService imageDeleteService)
    {
        _advertisementRepository = advertisementRepository;
        _currencyRepository = currencyRepository;
        _categoryRepository = categoryRepository;
        _imageQueryRepository = imageQueryRepository;
        _conditionRepository = conditionRepository;
        _userRepository = userRepository;
        _imageCreateService = imageCreateService;
        _imageDeleteService = imageDeleteService;
    }

    public async Task<Result<AdvertisementUpdateResponse>> UpdateAdvertisementAsync(
        int advertId,
        AdvertisementUpdateRequest request)
    {
        var advert = await _advertisementRepository.GetByIdAsync(advertId);
        if (advert is null)
            return Result<AdvertisementUpdateResponse>.NotFound("Advertisement");

        var validation = await ValidateAsync(request);
        if (!validation.IsSuccess)
            return Result<AdvertisementUpdateResponse>.Fail(validation.Error!);

        var updatedCount = await ApplyUpdates(advert, request);
        var result = new AdvertisementUpdateResponse(
                DateTime.UtcNow,
                updatedCount
            );

        if (updatedCount == 0)
            return Result<AdvertisementUpdateResponse>.Success(result);

        await _advertisementRepository.UpdateAsync(advert);

        return Result<AdvertisementUpdateResponse>.Success(result);
    }

    private async Task<Result<AdvertisementUpdateValidationResult>> ValidateAsync(
        AdvertisementUpdateRequest request)
    {
        User? buyer = null;
        if (request.BuyerId is not null)
        {
            buyer = await _userRepository.GetByIdAsync(request.BuyerId.Value);
            if (buyer is null)
                return Result<AdvertisementUpdateValidationResult>
                    .NotFound("Buyer");
        }

        Currency? currency = null;
        if (request.CurrencyId is not null)
        {
            currency = await _currencyRepository.GetByIdAsync(request.CurrencyId.Value);
            if (currency is null)
                return Result<AdvertisementUpdateValidationResult>
                    .NotFound("Currency");
        }

        Condition? condition = null;
        if (request.ConditionId is not null)
        {
             condition = await _conditionRepository.GetByIdAsync(request.ConditionId.Value);
            if (condition is null)
                return Result<AdvertisementUpdateValidationResult>
                    .NotFound("Condition");
        }

        Category? category = null;
        if (request.CategoryId is not null)
        {
            category = await _categoryRepository.GetByIdAsync(request.CategoryId.Value);
            if (category is null)
                return Result<AdvertisementUpdateValidationResult>
                    .NotFound("Category");
        }

        List<Image>? images = null;
        if (request.DeletedImageIds is not null)
        {
             images = await _imageQueryRepository.GetByIdsAsync(request.DeletedImageIds);

            if (images.Count != request.DeletedImageIds.Count)
            {
                var missing = request.DeletedImageIds.Except(images.Select(x => x.Id));

                return Result<AdvertisementUpdateValidationResult>
                    .NotFound($"Images: {string.Join(", ", missing)}");
            }
        }
        return Result<AdvertisementUpdateValidationResult>
            .Success(new (buyer, currency, condition, category, images));
    }

    private async Task<int> ApplyUpdates(
        Advertisement advert,
        AdvertisementUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.Title is not null)
        {
            advert.Title = request.Title;
            countUpdated++;
        }

        if (request.Description is not null)
        {
            advert.Description = request.Description;
            countUpdated++;
        }

        if (request.Price is not null)
        {
            var newPrice = request.Price.Value;

            if (newPrice < advert.Price)
                advert.PreviousPrice = advert.Price;
            else
                advert.PreviousPrice = newPrice;

            advert.Price = newPrice;
            countUpdated++;
        }

        if (request.Popularity is not null)
        {
            advert.Popularity = request.Popularity.Value;
            countUpdated++;
        }

        if (request.CurrencyId is not null)
        {
            advert.CurrencyId = request.CurrencyId.Value;
            countUpdated++;
        }

        if (request.ConditionId is not null)
        {
            advert.ConditionId = request.ConditionId.Value;
            countUpdated++;
        }

        if (request.CategoryId is not null)
        {
            advert.CategoryId = request.CategoryId.Value;
            countUpdated++;
        }

        if (request.BuyerId is not null)
        {
            advert.BuyerId = request.BuyerId.Value;
            countUpdated++;
        }

        if (request.DeletedImageIds is not null &&
            request.DeletedImageIds.Any())
        {
            foreach(var imageId in request.DeletedImageIds)
            {
                await _imageDeleteService.DeleteImageAsync(imageId);
            }

            countUpdated++;
        }

        if (request.NewImages is not null &&
            request.NewImages.Any())
        {
            foreach (var image in request.NewImages)
            {
                if (image is not null)
                    await _imageCreateService.CreateFromFileAsync(
                        new ImageFileCreateRequest(image, advert.Id));
            }

            countUpdated++;
        }


        if (request.BuyerId is not null)
        {
            advert.BuyerId = request.BuyerId.Value;
            countUpdated++;
        }
       

        if (request.Properties is not null)
        {
            advert.Properties = request.Properties;
            countUpdated++;
        }

        return countUpdated;
    }
}