using ShagOxServer.Application.DTOs.Advertisement.Add;
using ShagOxServer.Application.Interfaces.Advertisement.Add;
using ShagOxServer.Application.Interfaces.Validators;
using ShagOxServer.Domain.Entities;
using ShagOxServer.Infrastructure.Interfaces.Auth;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries;
using ShagOxServer.Infrastructure.Interfaces.Specification;

namespace ShagOxServer.Application.Services.Advertisements.Add;
public class AdvertisementAddService : IAdvertisementAddService
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ICategoryRepository _categoryRepository;


    public AdvertisementAddService(
        IUserRepository userRepository,
        ICurrencyRepository currencyRepository,
        ICategoryRepository categoryRepository)
    {
        _userRepository = userRepository;
        _currencyRepository = currencyRepository;
        _categoryRepository = categoryRepository;     
    }

    public async Task<AdvertisementAddResponse> AddAdvertisement(
        AdvertisementAddRequest request)
    {
        var seller = await _userRepository.GetByIdAsync(request.SellerId);
        if (seller is null)
            throw new ArgumentNullException(nameof(seller));

        var currency = await _currencyRepository.GetByIdAsync(request.CurrencyId);
        if (currency is null)
            throw new ArgumentNullException(nameof(seller));

        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
        if (category is null)
            throw new ArgumentNullException(nameof(seller));


        Advertisement advert = new Advertisement()
        {
            Title = request.Title,
            Description = request.Description ?? "",
            Popularity = request.Popularity,

            CurrencyId = request.CurrencyId,
            Currency = currency,

            CategoryId = request.CategoryId,
            Category = category,

            SellerId = seller.Id,
            Seller = seller,

            Properties = request.Property
        };

        return new AdvertisementAddResponse(true, "Add Advertisement successful", advert.CreatedAt);
    }
}
