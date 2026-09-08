using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Core.Validator;
public class AdvertisementValidator
    : BaseValidator<Advertisement>
{
    private readonly IAdvertisementQueryRepository _advertisementQueryRepository;


    public AdvertisementValidator(
        IRepository<Advertisement> advertisementRepository,
        IAdvertisementQueryRepository advertisementQueryRepository,
        IAdvertisementExistsRepository advertisementExistsRepository
    ) : base(advertisementRepository, advertisementExistsRepository)
    {
        _advertisementQueryRepository = advertisementQueryRepository;
    }


    public async Task<Result<Advertisement>> GetByIdWithIncludeAsync(
        int advertId)
    {
        var advert = await _advertisementQueryRepository
            .GetByIdAsync(advertId);

        if (advert is null)
        {
            return Result<Advertisement>
                .NotFound(typeof(Advertisement));
        }

        return Result<Advertisement>.Success(advert);
    }
}