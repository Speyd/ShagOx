using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Advertisements.Create;
using ShagOxServer.Application.Interfaces.Advertisements.Create;
using ShagOxServer.Domain.Entities;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Infrastructure.Interfaces.Advertisements;
using ShagOxServer.Infrastructure.Interfaces.Auth;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries;
using ShagOxServer.Infrastructure.Interfaces.Specification;
using System.Text.Json;

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

    public async Task<Result<AdvertisementCreateResponse>> CreateAdvertisementAsync(
        AdvertisementCreateRequest request)
    {
        var validation = await ValidateAsync(request);

        if (!validation.IsSuccess)
            return Result<AdvertisementCreateResponse>.Fail(validation.Error!);

        var (seller, currency, category) = validation.Value!;

        var advert = CreateAdvertisement(request, seller, currency, category);

        await _advertisementRepository.AddAsync(advert);

        var response = new AdvertisementCreateResponse(
            advert.Id,
            advert.CreatedAt
        );

        return Result<AdvertisementCreateResponse>.Success(response);
    }

    private async Task<Result<(User seller, Currency currency, Category category)>> ValidateAsync(
        AdvertisementCreateRequest request)
    {
        var seller = await _userRepository.GetByIdAsync(request.SellerId);
        if (seller is null)
            return Result<(User, Currency, Category)>.NotFound("Seller");

        var currency = await _currencyRepository.GetByIdAsync(request.CurrencyId);
        if (currency is null)
            return Result<(User, Currency, Category)>.NotFound("Currency");

        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
        if (category is null)
            return Result<(User, Currency, Category)>.NotFound("Category");

        return Result<(User, Currency, Category)>.Success((seller, currency, category));
    }

    private Advertisement CreateAdvertisement(
        AdvertisementCreateRequest request,
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

            Properties = request.Properties ?? new()
        };
    }
}
