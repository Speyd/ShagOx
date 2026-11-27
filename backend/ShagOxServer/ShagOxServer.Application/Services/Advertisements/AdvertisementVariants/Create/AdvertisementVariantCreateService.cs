using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Create;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions.Query;
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

    private readonly IAdvertisementQueryRepository _advertRepository;
    private readonly AdvertisementInvalidationService _advertInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AdvertisementVariantCreateService> _logger;


    public AdvertisementVariantCreateService(
        IRepository<AdvertisementVariant> variantRepository,
        AdvertisementVariantValidator variantValidator,
        AdvertisementValidator advertValidator,
        IAttributeDefinitionQueryRepository attributeRepository,
        IAdvertisementQueryRepository advertRepository,
        AdvertisementInvalidationService advertInvalid,
        IUnitOfWork unitOfWork,
        ILogger<AdvertisementVariantCreateService> logger)
    {
        _advertRepository = advertRepository;
        _advertInvalid = advertInvalid;
        _variantRepository = variantRepository;
        _variantValidator = variantValidator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        AdvertisementVariantCreateRequest request,
        CancellationToken cancellationToken = default)
    {
        Result<CreateResponse> createResult;

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            createResult = await CreateInternalAsync(
                request, cancellationToken);

            if (!createResult.IsSuccess)
            {
                await _unitOfWork.RollbackAsync();
                return createResult;
            }

            await _unitOfWork.CommitAsync();
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

        // Оголошення кешується разом зі списком варіантів.
        var advertInfo = await _advertRepository
            .GetCacheInfoByIdAsync(request.AdvertisementId,
                cancellationToken, EntityStatus.All);

        if (advertInfo is not null)
            await _advertInvalid.InvalidateUpdateAsync(advertInfo);

        return createResult;
    }

    public async Task<Result<CreateResponse>> CreateInternalAsync(
        AdvertisementVariantCreateRequest request,
        CancellationToken cancellationToken = default)
    {
        var variantValidation = await _variantValidator
            .NotExistsAsync(
                request.AdvertisementId,
                request.Attributes,
                cancellationToken);

        if (!variantValidation.IsSuccess)
        {
            return Result<CreateResponse>
                .Fail(variantValidation.Error);
        }

        var isVariantExists = await _variantValidator
            .ValidateVariantAttributesAsync(
                request.AdvertisementId,
                request.Attributes,
                cancellationToken);

        if (!isVariantExists.IsSuccess)
        {
            return Result<CreateResponse>
                .Fail(isVariantExists.Error);
        }


        var variant = AdvertisementVariantCreater.Create(request);

        _variantRepository.Add(variant);

        await _unitOfWork.SaveChangesAsync();

        return Result<CreateResponse>.Success(
            new CreateResponse(
                variant.Id,
                DateTime.UtcNow));
    }
}
