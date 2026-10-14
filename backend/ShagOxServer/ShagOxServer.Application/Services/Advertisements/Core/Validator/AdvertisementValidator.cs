using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Core.Validator;
public class AdvertisementValidator
    : BaseValidator<Advertisement>
{
    private readonly IAdvertisementQueryRepository _advertisementQueryRepository;

    public AdvertisementValidator(
        IAdvertisementQueryRepository advertisementQueryRepository,
        IAdvertisementExistsRepository advertisementExistsRepository
    ) : base(advertisementQueryRepository, advertisementExistsRepository)
    {
        _advertisementQueryRepository = advertisementQueryRepository;
    }


    public async Task<Result<Advertisement>> GetByIdWithIncludeAsync(
        long advertId)
    {
        var advert = await _advertisementQueryRepository
            .GetByIdIncludeAsync(advertId);

        if (advert is null)
        {
            return Result<Advertisement>.NotFound(
                EntityNamesResources.Advertisement);
        }

        return Result<Advertisement>.Success(advert);
    }
}
