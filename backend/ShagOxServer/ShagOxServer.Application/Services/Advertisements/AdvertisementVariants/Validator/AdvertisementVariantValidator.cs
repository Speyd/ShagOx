using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System.Text.Json;

namespace ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Validator;
public class AdvertisementVariantValidator
    : BaseValidator<AdvertisementVariant>
{
    private readonly IAdvertisementVariantExistsRepository _variantExistsRepository;


    public AdvertisementVariantValidator(
        IRepository<AdvertisementVariant> variantRepository,
        IAdvertisementVariantExistsRepository variantExistsRepository
    ) : base(variantRepository, variantExistsRepository)
    {
        _variantExistsRepository = variantExistsRepository;
    }


    public async Task<Result<bool>> ExistsAsync(
        long advertisementId,
        JsonDocument attributes)
    {
        if (!await _variantExistsRepository
                .ExistsAsync(advertisementId, attributes))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.AdvertisementVariant);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsAsync(
       long advertisementId,
       JsonDocument attributes)
    {
        if (await _variantExistsRepository
                .ExistsAsync(advertisementId, attributes))
        {
            return Result<bool>.AlreadyExists(
                EntityNamesResources.AdvertisementVariant);
        }

        return Result<bool>.Success(true);
    }
}