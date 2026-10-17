using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Advertisements.Core.Create;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Create;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Validator;
using ShagOxServer.Application.Services.Advertisements.Core.Create.Validator;
using ShagOxServer.Application.Services.Advertisements.Core.Mapping;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Core.Create;
public partial class AdvertisementCreateService 
    : IAdvertisementCreateService
{
    private readonly IRepository<Advertisement> _advertRepository;
    private readonly AdvertisementCreateValidator _advertCreateValidator;
    private readonly AdvertisementVariantValidator _variantValidator;

    private readonly AdvertisementInvalidationService _advertInvalid;

    private readonly IAdvertisementVariantCreateService _variantCreateService;

    private readonly IAdvertisementImageCreateService _imageCreateService;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AdvertisementCreateService> _logger;


    public AdvertisementCreateService(
        IRepository<Advertisement> advertRepository,
        AdvertisementCreateValidator advertCreateValidator,
        AdvertisementVariantValidator variantValidator,
        AdvertisementInvalidationService advertInvalid,
        IAdvertisementVariantCreateService variantCreateService,
        IAdvertisementImageCreateService imageCreateService,
        IUnitOfWork unitOfWork,
        ILogger<AdvertisementCreateService> logger)
    {
        _advertRepository = advertRepository;
        _advertCreateValidator = advertCreateValidator;
        _variantValidator = variantValidator;
        _variantCreateService = variantCreateService;
        _advertInvalid = advertInvalid;

        _imageCreateService = imageCreateService;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        AdvertisementCreateRequest request,
        long ownerId)
    {
        var validation = await _advertCreateValidator
            .ValidateAsync(request, ownerId);

        if (!validation.IsSuccess)
            return Result<CreateResponse>.Fail(validation.Error);

        var variantsResult = ParseAndValidateVariants(request.Variants);

        if (!variantsResult.IsSuccess)
        {
            return Result<CreateResponse>
                .Fail(variantsResult.Error!);
        }


        var advert = AdvertisementCreater
            .Create(request, ownerId);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _advertRepository.Add(advert);

            await _unitOfWork.SaveChangesAsync();

            var createVariant = await CreateVariantsAsync(
                ownerId,
                advert.Id,
                variantsResult.Value);

            if (!createVariant.IsSuccess)
            {
                await _unitOfWork.RollbackAsync();

                return Result<CreateResponse>
                    .Fail(createVariant.Error);
            }

            var imagesResult = await _imageCreateService
                .CreateAsync(
                    new PicturesCreateRequest(
                        advert.Id,
                        request.Pictures
                    )
            );

            if (!imagesResult.IsSuccess)
            {
                await _unitOfWork.RollbackAsync();

                return Result<CreateResponse>
                    .Fail(imagesResult.Error!);
            }


            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to create advertisement. " +
                "UserId: {UserId}, Title: {Title}",
                ownerId,
                request.Title);

            return Result<CreateResponse>
                    .Fail(EntityErrorResources.AdvertisementCreateFailed);
        }

        _logger.LogInformation(
            "Advertisement created successfully. Id: {Id}",
            advert.Id);

        await _advertInvalid.InvalidateCreateAsync(
            AdvertisementCacheMapper.ToInfo(advert));

        return Result<CreateResponse>.Success(
            new CreateResponse(
                advert.Id,
                advert.CreatedAt
        ));
    }
}
