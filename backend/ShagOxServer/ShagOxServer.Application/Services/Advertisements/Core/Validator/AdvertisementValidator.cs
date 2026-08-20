using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Core.Validator;
public class AdvertisementValidator
{
    private readonly IRepository<Advertisement> _advertisementRepository;

    private readonly IAdvertisementQueryRepository _advertisementQueryRepository;

    private readonly IAdvertisementExistsRepository _advertisementExistsRepository;


    public AdvertisementValidator(
        IRepository<Advertisement> advertisementRepository,
        IAdvertisementQueryRepository advertisementQueryRepository,
        IAdvertisementExistsRepository advertisementExistsRepository)
    {
        _advertisementRepository = advertisementRepository;
        _advertisementQueryRepository = advertisementQueryRepository;
        _advertisementExistsRepository = advertisementExistsRepository;
    }

    public async Task<Result<Advertisement>> GetByIdAsync(
        int advertId)
    {
        var advert = await _advertisementRepository
            .GetByIdAsync(advertId);

        if (advert is null)
            return Result<Advertisement>.NotFound("Advertisement");

        return Result<Advertisement>.Success(advert);
    }

    public async Task<Result<Advertisement>> GetByIdWithIncludeAsync(
        int advertId)
    {
        var advert = await _advertisementQueryRepository
            .GetByIdAsync(advertId);

        if (advert is null)
            return Result<Advertisement>.NotFound("Advertisement");

        return Result<Advertisement>.Success(advert);
    }

    public async Task<Result<bool>> ExistsByIdAsync(
        int advertId)
    {
        if (!await _advertisementExistsRepository.ExistsByIdAsync(advertId))
            return Result<bool>.NotFound("Advertisement");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByIdAsync(
        int advertId)
    {
        if (await _advertisementExistsRepository.ExistsByIdAsync(advertId))
            return Result<bool>.AlreadyExists("Advertisement");

        return Result<bool>.Success(true);
    }
}