using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Advertisements.Update;
using ShagOxServer.Application.Interfaces.Advertisements.Update;
using ShagOxServer.Domain.Entities;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Infrastructure.Interfaces.Advertisements;
using ShagOxServer.Infrastructure.Interfaces.Auth;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries;
using ShagOxServer.Infrastructure.Interfaces.Specification;

namespace ShagOxServer.Application.Services.Advertisements.Update;
public class AdvertisementUpdateService : IAdvertisementUpdateService
{
    private readonly IAdvertisementRepository _advertisementRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IConditionRepository _conditionRepository;
    private readonly IImageRepository _imageRepository;

    public AdvertisementUpdateService(
        IAdvertisementRepository advertisementRepository,
        IUserRepository userRepository,
        ICurrencyRepository currencyRepository,
        ICategoryRepository categoryRepository,
        IImageRepository imageRepository,
        IConditionRepository conditionRepository)
    {
        _advertisementRepository = advertisementRepository;
        _currencyRepository = currencyRepository;
        _categoryRepository = categoryRepository;
        _imageRepository = imageRepository;
        _conditionRepository = conditionRepository;
        _userRepository = userRepository;
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

        var (buyer, currency, condition, category, images) = validation.Value!;

        var updatedCount = ApplyUpdates(advert, images, request);
        var result = new AdvertisementUpdateResponse(
                DateTime.UtcNow,
                updatedCount
            );

        if (updatedCount == 0)
            return Result<AdvertisementUpdateResponse>.Success(result);

        await _advertisementRepository.UpdateAsync(advert);

        return Result<AdvertisementUpdateResponse>.Success(result);
    }

    private async Task<Result<(
        User? buyer,
        Currency? currency,
        Condition? condition,
        Category? category,
    List<Image>? images)>> ValidateAsync(
        AdvertisementUpdateRequest request)
    {
        User? buyer = null;
        if (request.BuyerId is not null)
        {
            buyer = await _userRepository.GetByIdAsync(request.BuyerId.Value);
            if (buyer is null)
                return Result<(User?, Currency?, Condition?, Category?, List<Image>?)>
                    .NotFound("Buyer");
        }

        Currency? currency = null;
        if (request.CurrencyId is not null)
        {
            currency = await _currencyRepository.GetByIdAsync(request.CurrencyId.Value);
            if (currency is null)
                return Result<(User?, Currency ?, Condition?, Category?, List<Image>?)>
                    .NotFound("Currency");
        }

        Condition? condition = null;
        if (request.ConditionId is not null)
        {
             condition = await _conditionRepository.GetByIdAsync(request.ConditionId.Value);
            if (condition is null)
                return Result<(User?, Currency?, Condition?, Category?, List<Image>?)>
                    .NotFound("Condition");
        }

        Category? category = null;
        if (request.CategoryId is not null)
        {
            category = await _categoryRepository.GetByIdAsync(request.CategoryId.Value);
            if (category is null)
                return Result<(User?, Currency?, Condition?, Category?, List<Image>?)>
                    .NotFound("Category");
        }

        List<Image>? images = null;
        if (request.Images is not null)
        {
             images = await _imageRepository.GetByIdsAsync(request.Images);

            if (images.Count != request.Images.Count)
            {
                var missing = request.Images.Except(images.Select(x => x.Id));

                return Result<(User?, Currency?, Condition?, Category?, List<Image>?)>
                    .NotFound($"Images: {string.Join(", ", missing)}");
            }
        }
        return Result<(User?, Currency?, Condition?, Category?, List<Image>?)>
            .Success((buyer, currency, condition, category, images));
    }

    private static int ApplyUpdates(
        Advertisement advert,
        List<Image>? images,
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

        if (images is not null)
        {
            advert.Images = images;
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