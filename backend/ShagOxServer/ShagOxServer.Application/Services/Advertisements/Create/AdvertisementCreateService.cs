using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Advertisements.Create;
using ShagOxServer.Application.Interfaces.Advertisements.Create;
using ShagOxServer.Domain.Entities;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Infrastructure.Interfaces.Advertisements;
using ShagOxServer.Infrastructure.Interfaces.Auth.Users;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries.Categories;
using ShagOxServer.Infrastructure.Interfaces.Specification;

namespace ShagOxServer.Application.Services.Advertisements.Create;
public class AdvertisementCreateService : IAdvertisementCreateService
{
    private readonly IAdvertisementRepository _advertisementRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IConditionRepository _conditionRepository;
    private readonly IImageRepository _imageRepository;

    public AdvertisementCreateService(
        IAdvertisementRepository advertisementRepository,
        IUserRepository userRepository,
        ICurrencyRepository currencyRepository,
        ICategoryRepository categoryRepository,
        IImageRepository imageRepository,
        IConditionRepository conditionRepository)
    {
        _advertisementRepository = advertisementRepository;
        _userRepository = userRepository;
        _currencyRepository = currencyRepository;
        _categoryRepository = categoryRepository;
        _imageRepository = imageRepository;
        _conditionRepository = conditionRepository;
    }

    public async Task<Result<AdvertisementCreateResponse>> CreateAdvertisementAsync(
        AdvertisementCreateRequest request)
    {
        var validation = await ValidateAsync(request);

        if (!validation.IsSuccess)
            return Result<AdvertisementCreateResponse>.Fail(validation.Error!);

        var (seller, currency, condition, category, images) = validation.Value!;

        var advert = CreateAdvertisement(images, request);

        await _advertisementRepository.AddAsync(advert);

        var response = new AdvertisementCreateResponse(
            advert.Id,
            advert.CreatedAt
        );

        return Result<AdvertisementCreateResponse>.Success(response);
    }

    private async Task<Result<(
        User seller,
        Currency currency,
        Condition condition,
        Category category,
    List<Image> images)>> ValidateAsync(
        AdvertisementCreateRequest request)
    {
        var seller = await _userRepository.GetByIdAsync(request.SellerId);
        if (seller is null)
            return Result<(User, Currency, Condition, Category, List<Image>)>
                .NotFound("Seller");

        var currency = await _currencyRepository.GetByIdAsync(request.CurrencyId);
        if (currency is null)
            return Result<(User, Currency, Condition, Category, List<Image>)>
                .NotFound("Currency");

        var condition = await _conditionRepository.GetByIdAsync(request.ConditionId);
        if (condition is null)
            return Result<(User, Currency, Condition, Category, List<Image>)>
                .NotFound("Condition");

        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
        if (category is null)
            return Result<(User, Currency, Condition, Category, List<Image>)>
                .NotFound("Category");

        var images = await _imageRepository.GetByIdsAsync(request.Images);

        if (images.Count != request.Images.Count)
        {
            var missing = request.Images.Except(images.Select(x => x.Id));

            return Result<(User, Currency, Condition, Category, List<Image>)>
                .NotFound($"Images: {string.Join(", ", missing)}");
        }

        return Result<(User, Currency, Condition, Category, List<Image>)>
            .Success((seller, currency, condition, category, images));
    }

    private Advertisement CreateAdvertisement(
        List<Image> images,
        AdvertisementCreateRequest request)
    {
        return new Advertisement
        {
            Title = request.Title,
            Description = request.Description ?? "",
            Popularity = request.Popularity,

            CurrencyId = request.CurrencyId,
            CategoryId = request.CategoryId,
            SellerId = request.SellerId,

            Images = images,

            Properties = request.Properties ?? new()
        };
    }
}
