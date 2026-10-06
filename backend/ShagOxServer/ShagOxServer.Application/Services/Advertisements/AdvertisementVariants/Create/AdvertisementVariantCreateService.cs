using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Create;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Validator;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Create;
public class AdvertisementVariantCreateService
    : IAdvertisementVariantCreateService
{
    private readonly IRepository<AdvertisementVariant> _variantRepository;
    private readonly AdvertisementVariantValidator _variantValidator;

    private readonly AdvertisementVariantInvalidationService _variantInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AdvertisementVariantCreateService> _logger;


    public AdvertisementVariantCreateService(
        IRepository<AdvertisementVariant> variantRepository,
        AdvertisementVariantValidator variantValidator,
        AdvertisementValidator advertValidator,
        AdvertisementVariantInvalidationService variantInvalid,
        IAttributeDefinitionQueryRepository attributeRepository,
        IUnitOfWork unitOfWork,
        ILogger<AdvertisementVariantCreateService> logger)
    {
        _variantRepository = variantRepository;
        _variantValidator = variantValidator;
        _variantInvalid = variantInvalid;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        AdvertisementVariantCreateRequest request)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var result = await CreateInternalAsync(request);

            if (!result.IsSuccess)
            {
                await _unitOfWork.RollbackAsync();
                return result;
            }

            await _unitOfWork.CommitAsync();

            return result;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to create advertisement variant. " +
                "AdvertisementId: {AdvertisementId}",
                request.AdvertisementId);

            return Result<CreateResponse>
                .Fail(EntityErrorResources.AdvertisementVariantCreateFailed);
        }
    }

    public async Task<Result<CreateResponse>> CreateInternalAsync(
        AdvertisementVariantCreateRequest request)
    {
        var variantValidation = await _variantValidator
            .NotExistsAsync(
                request.AdvertisementId,
                request.Attributes);

        if (!variantValidation.IsSuccess)
        {
            return Result<CreateResponse>
                .Fail(variantValidation.Error);
        }

        var isVariantExists = await _variantValidator
            .ValidateVariantAttributesAsync(
                request.AdvertisementId,
                request.Attributes);

        if (!isVariantExists.IsSuccess)
        {
            return Result<CreateResponse>
                .Fail(isVariantExists.Error);
        }


        var variant = AdvertisementVariantCreater.Create(request);

        _variantRepository.Add(variant);

        await _unitOfWork.SaveChangesAsync();

        await _variantInvalid.InvalidateCreateAsync(variant.Id);

        return Result<CreateResponse>.Success(
            new CreateResponse(
                variant.Id,
                DateTime.UtcNow));
    }
}