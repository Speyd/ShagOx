using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Create;
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Update;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Resources.Validations;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System.Text.Json;

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
        long advertId)
    {
        var advert = await _advertisementQueryRepository
            .GetByIdAsync(advertId);

        if (advert is null)
        {
            return Result<Advertisement>.NotFound(
                EntityNamesResources.Advertisement);
        }

        return Result<Advertisement>.Success(advert);
    }

    public Result<bool> HasDuplicateAttributes(
        Dictionary<long, AdvertisementVariantUpdateRequest> variants)
    {
        var attributes = variants
            .Select(x => x.Value.Attributes)
            .ToList();

        if (HasDuplicate(attributes))
        {
            return Result<bool>.Fail(
                ValidationResources.AdvertisementVariantAttributesMustBeUnique);
        }

        return Result<bool>.Success(true);
    }

    public Result<bool> HasDuplicateAttributes(
        IList<AdvertisementVariantCreateRequest> variants)
    {
        var attributes = variants
            .Select(x => x.Attributes)
            .ToList();

        if (HasDuplicate(attributes))
        {
            return Result<bool>.Fail(
                ValidationResources.AdvertisementVariantAttributesMustBeUnique);
        }

        return Result<bool>.Success(true);
    }

    private bool HasDuplicate(
        IEnumerable<JsonDocument?>? attributes)
    {
        if (attributes is null)
            return false;

        var documents = attributes
            .Where(x => x is not null)
            .Select(x => x!.RootElement)
            .ToList();

        for (var i = 0; i < documents.Count; i++)
        {
            for (var j = i + 1; j < documents.Count; j++)
            {
                if (JsonElement.DeepEquals(
                        documents[i],
                        documents[j]))
                {
                    return true;
                }
            }
        }

        return false;
    }
}