using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Validator;
public class AdvertisementValidator
{
    private readonly IAdvertisementRepository _advertisementRepository;


    public AdvertisementValidator(
        IAdvertisementRepository advertisementRepository)
    {
        _advertisementRepository = advertisementRepository;
    }

    public async Task<Result<Advertisement>> GetAdvertisementValidator(
        int advertId)
    {
        var advert = await _advertisementRepository.GetByIdAsync(advertId);
        if (advert is null)
            return Result<Advertisement>.NotFound("Advertisement");

        return Result<Advertisement>.Success(advert);
    }
}
