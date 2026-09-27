using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Create;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Validator;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;
namespace ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Create;

public class AdvertisementVariantCreateService
    : IAdvertisementVariantCreateService
{
    private readonly IRepository<AdvertisementVariant> _variantRepository;
    private readonly AdvertisementVariantValidator _variantValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AdvertisementVariantCreateService> _logger;


    public AdvertisementVariantCreateService(
        IRepository<AdvertisementVariant> variantRepository,
        AdvertisementVariantValidator variantValidator,
        IUnitOfWork unitOfWork,
        ILogger<AdvertisementVariantCreateService> logger)
    {
        _variantRepository = variantRepository;
        _variantValidator = variantValidator;
        _logger = logger;

        _unitOfWork = unitOfWork;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        AdvertisementVariantCreateRequest request)
    {
        var variantValidation = await _variantValidator
            .NotExistsAsync(request.AdvertisementId, request.Attributes);

        if (!variantValidation.IsSuccess)
        {
            return Result<CreateResponse>
                .Fail(variantValidation.Error);
        }

        var variant = AdvertisementVariantCreater.Create(request);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _variantRepository.Add(variant);

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

        return Result<CreateResponse>.Success(
            new CreateResponse(
                variant.Id,
                DateTime.UtcNow
        ));
    }
}