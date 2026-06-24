using ShagOxServer.Application.DTOs.Advertisement.Add;
using ShagOxServer.Application.Interfaces.Advertisement.Add;
using ShagOxServer.Application.Interfaces.Validators;
using ShagOxServer.Domain.Entities;
using ShagOxServer.Infrastructure.Interfaces.Auth;

namespace ShagOxServer.Application.Services.Advertisements.Add;
public class AdvertisementAddService : IAdvertisementAddService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IContactValidator _contactValidator;


    public AdvertisementAddService(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IContactValidator contactValidator)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _contactValidator = contactValidator;
    }

    public async Task<AdvertisementAddResponse> AddAdvertisement(
        AdvertisementAddRequest request)
    {
        var seller = _userRepository.GetByIdAsync(request.SellerId).Result;
        if (seller is null)
            throw new ArgumentNullException(nameof(seller));


        Advertisement advert = new Advertisement()
        {
            Title = request.Title,
            Description = request.Description ?? "",
            Popularity = request.Popularity,

            CurrencyId = request.CurrencyId,
            CategoryId = request.CategoryId,

            SellerId = seller.Id,
            Seller = seller,

            Properties = request.Property
        };

        return new AdvertisementAddResponse(true, "Add Advertisement successful", advert.CreatedAt);
    }
}
