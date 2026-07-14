using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Validator;
public class AdvertisementValidator
{
    private readonly IAdvertisementRepository _advertisementRepository;
    private readonly IAdvertisementExistsRepository _advertisementExistsRepository;


    public AdvertisementValidator(
        IAdvertisementRepository advertisementRepository,
        IAdvertisementExistsRepository advertisementExistsRepository)
    {
        _advertisementRepository = advertisementRepository;
        _advertisementExistsRepository = advertisementExistsRepository;
    }

    public async Task<Result<Advertisement>> GetAdvertisementValidator(
        int advertId)
    {
        var advert = await _advertisementRepository.GetByIdAsync(advertId);
        if (advert is null)
            return Result<Advertisement>.NotFound("Advertisement");

        return Result<Advertisement>.Success(advert);
    }

    public async Task<Result<bool>> ExistsAdvertisementValidator(
        int advertId)
    {
        var advert = await _advertisementExistsRepository.ExistsById(advertId);
        if (advert)
            return Result<bool>.AlreadyExists("Advertisement");

        return Result<bool>.Success(advert);
    }
}
