using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Create;
using ShagOxServer.Application.DTOs.Advertisements.Core.Create;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Specification.Pictures.Images.Create.File;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Create;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Resources.Validations;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Validator;
using ShagOxServer.Application.Services.Advertisements.Core.Create.Validator;
using ShagOxServer.Application.Services.Advertisements.Core.Mapping;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System.Text.Json;

namespace ShagOxServer.Application.Services.Advertisements.Core.Create;
public class AdvertisementCreateService 
    : IAdvertisementCreateService
{
    private readonly IRepository<Advertisement> _advertRepository;
    private readonly AdvertisementCreateValidator _advertCreateValidator;
    private readonly AdvertisementVariantValidator _variantValidator;

    private readonly AdvertisementInvalidationService _advertInvalid;

    private readonly IAdvertisementVariantCreateService _variantCreateService;

    private readonly IImageCreateService _imageCreateService;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AdvertisementCreateService> _logger;


    public AdvertisementCreateService(
        IRepository<Advertisement> advertRepository,
        AdvertisementCreateValidator advertCreateValidator,
        AdvertisementVariantValidator variantValidator,
        AdvertisementInvalidationService advertInvalid,
        IAdvertisementVariantCreateService variantCreateService,
        IImageCreateService imageCreateService,
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
        long userId)
    {
        var validation = await _advertCreateValidator
            .ValidateAsync(request);

        if (!validation.IsSuccess)
            return Result<CreateResponse>.Fail(validation.Error);

        var variantsResult = ParseAndValidateVariants(request.Variants);

        if (!variantsResult.IsSuccess)
        {
            return Result<CreateResponse>
                .Fail(variantsResult.Error!);
        }


        var advert = AdvertisementCreater
            .Create(request, userId);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _advertRepository.Add(advert);

            await _unitOfWork.SaveChangesAsync();

            var createVariant = await CreateVariantsAsync(
                userId,
                advert.Id,
                variantsResult.Value);

            if (!createVariant.IsSuccess)
            {
                await _unitOfWork.RollbackAsync();

                return Result<CreateResponse>
                    .Fail(createVariant.Error);
            }

            var imagesResult = await _imageCreateService
                .CreateFromFilesAsync(
                    new ImageFilesCreateRequest(
                        advert.Id,
                        request.Images
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
                userId,
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

    private Result<List<AdvertisementVariantCreateRequest>?> ParseAndValidateVariants(
        string? variantsJson)
    {
        if (string.IsNullOrWhiteSpace(variantsJson))
        {
            return Result<List<AdvertisementVariantCreateRequest>?>
                .Success(null);
        }

        try
        {
            if (string.IsNullOrWhiteSpace(variantsJson))
            {
                return Result<List<AdvertisementVariantCreateRequest>?>
                    .Success(null);
            }

            var variants = JsonSerializer.Deserialize<
                List<AdvertisementVariantCreateRequest>
                    >(variantsJson);

            if (variants is null)
            {
                return Result<List<AdvertisementVariantCreateRequest>?>
                    .Success(null);
            }

            var duplicateExists = _variantValidator
                .ValidateUniqueAttributes(
                    variants
                        .Select(x => x.Attributes)
                        .ToList()
                );

            if (!duplicateExists.IsSuccess)
            {
                return Result<List<AdvertisementVariantCreateRequest>?>
                    .Fail(duplicateExists.Error!);
            }

            return Result<List<AdvertisementVariantCreateRequest>?>
                .Success(variants);
        }
        catch (JsonException)
        {
            return Result<List<AdvertisementVariantCreateRequest>?>
                .Fail(ValidationResources.InvalidJson);
        }
    }

    private async Task<Result<bool>> CreateVariantsAsync(
        long userId,
        long advertisementId,
        IEnumerable<AdvertisementVariantCreateRequest>? variants)
    {
        if (variants is null)
            return Result<bool>.Success(true);

        foreach (var variant in variants)
        {
            var newVariant = variant with
            {
                AdvertisementId = advertisementId
            };

            var result = await _variantCreateService
                .CreateInternalAsync(newVariant);

            if (!result.IsSuccess)
            {
                _logger.LogError(
                    "Failed to create advertisement(variant create). " +
                    "UserId: {UserId}",
                    userId);

                return Result<bool>.Fail(result.Error);
            }
        }

        return Result<bool>.Success(true);
    }
}