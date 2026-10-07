using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Resources.Validations;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System.Text.Json;

namespace ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Validator;
public class AdvertisementVariantValidator
    : BaseValidator<AdvertisementVariant>
{
    private readonly IAdvertisementVariantExistsRepository _variantExistsRepository;
    private readonly IAttributeDefinitionQueryRepository _attributeRepository;

    private readonly AdvertisementValidator _advertValidator;


    public AdvertisementVariantValidator(
        IQueryRepository<AdvertisementVariant> variantRepository,
        IAdvertisementVariantExistsRepository variantExistsRepository,
        IAttributeDefinitionQueryRepository attributeRepository,
        AdvertisementValidator advertValidator
    ) : base(variantRepository, variantExistsRepository)
    {
        _variantExistsRepository = variantExistsRepository;
        _advertValidator = advertValidator;
        _attributeRepository = attributeRepository;
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

    public async Task<Result<bool>> ValidateVariantAttributesAsync(
         long advertId,
         JsonDocument attributes)
    {
        var advert = await _advertValidator
            .GetByIdAsync(advertId);

        if (!advert.IsSuccess)
            return Result<bool>.Fail(advert.Error);

        var keys = attributes.RootElement
            .EnumerateObject()
            .Select(x => x.Name)
            .ToList();

        var result = await _attributeRepository
            .GetByKeysAsync(advert.Value!.CategoryId, keys);

        if (result is null)
        {
            return Result<bool>.NotFound(
                EntityNamesResources.AttributeDefinition);
        }

        foreach (var element in result)
        {
            if (!element.IsVariant)
            {
                return Result<bool>.Fail(
                    ValidationResources.AttributeMustBeVariant);
            }
        }

        return Result<bool>.Success(true);
    }

    public Result<bool> ValidateUniqueAttributes(
        IEnumerable<JsonDocument?> attributes)
    {
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
                    return Result<bool>.Fail(
                        ValidationResources.AdvertisementVariantAttributesMustBeUnique);
                }
            }
        }

        return Result<bool>.Success(true);
    }
}
