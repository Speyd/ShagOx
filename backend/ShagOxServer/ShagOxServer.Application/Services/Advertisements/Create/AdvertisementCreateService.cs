using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Advertisements.Create;
using ShagOxServer.Application.Interfaces.Advertisements.Create;
using ShagOxServer.Domain.Entities;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Infrastructure.Interfaces;
using ShagOxServer.Infrastructure.Interfaces.Auth;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries;
using ShagOxServer.Infrastructure.Interfaces.Specification;

namespace ShagOxServer.Application.Services.Advertisements.Create;
public class AdvertisementCreateService : IAdvertisementCreateService
{
    private readonly IAdvertisementRepository _advertisementRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ICategoryRepository _categoryRepository;


    public AdvertisementCreateService(
        IAdvertisementRepository advertisementRepository,
        IUserRepository userRepository,
        ICurrencyRepository currencyRepository,
        ICategoryRepository categoryRepository)
    {
        _advertisementRepository = advertisementRepository;
        _userRepository = userRepository;
        _currencyRepository = currencyRepository;
        _categoryRepository = categoryRepository;     
    }

    public async Task<Result<AdvertisementAddResponse>> AddAdvertisementAsync(
    AdvertisementAddRequest request)
    {
        var validation = await ValidateAsync(request);

        if (!validation.IsSuccess)
            return Result<AdvertisementAddResponse>.Fail(validation.Error!);

        var (seller, currency, category) = validation.Value!;

        var advert = CreateAdvertisement(request, seller, currency, category);

        await _advertisementRepository.AddAsync(advert);

        var response = new AdvertisementAddResponse(
            advert.Id,
            advert.CreatedAt
        );

        return Result<AdvertisementAddResponse>.Success(response);
    }

    private async Task<Result<(User seller, Currency currency, Category category)>> ValidateAsync(AdvertisementAddRequest request)
    {
        var seller = await _userRepository.GetByIdAsync(request.SellerId);
        if (seller is null)
            return Result<(User, Currency, Category)>.Fail("Seller not found");

        var currency = await _currencyRepository.GetByIdAsync(request.CurrencyId);
        if (currency is null)
            return Result<(User, Currency, Category)>.Fail("Currency not found");

        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
        if (category is null)
            return Result<(User, Currency, Category)>.Fail("Category not found");

        return Result<(User, Currency, Category)>.Success((seller, currency, category));
    }

    private Advertisement CreateAdvertisement(
        AdvertisementAddRequest request,
        User seller,
        Currency currency,
        Category category)
    {
        return new Advertisement
        {
            Title = request.Title,
            Description = request.Description ?? "",
            Popularity = request.Popularity,

            CurrencyId = currency.Id,
            CategoryId = category.Id,
            SellerId = seller.Id,

            Properties = request.Property
        };
    }
}
