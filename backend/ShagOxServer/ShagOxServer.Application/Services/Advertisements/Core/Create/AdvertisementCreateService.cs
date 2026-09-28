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
using ShagOxServer.Application.Services.Advertisements.Core.Create.Validator;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Core.Create;
public class AdvertisementCreateService 
    : IAdvertisementCreateService
{
    private readonly IRepository<Advertisement> _advertRepository;
    private readonly AdvertisementCreateValidator _advertCreateValidator;
    private readonly AdvertisementValidator _advertValidator;


    private readonly IAdvertisementVariantCreateService _variantCreateService;


    private readonly IImageCreateService _imageCreateService;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AdvertisementCreateService> _logger;


    public AdvertisementCreateService(
        IRepository<Advertisement> advertRepository,
        AdvertisementCreateValidator advertCreateValidator,
        AdvertisementValidator advertValidator,
        IAdvertisementVariantCreateService variantCreateService,
        IImageCreateService imageCreateService,
        IUnitOfWork unitOfWork,
        ILogger<AdvertisementCreateService> logger)
    {
        _advertRepository = advertRepository;
        _advertCreateValidator = advertCreateValidator;
        _advertValidator = advertValidator;
        _variantCreateService = variantCreateService;

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

        
        if (request.Variants is not null)
        {
            var duplicateExists = _advertValidator
                .HasDuplicateAttributes(request.Variants);

            if (!duplicateExists.IsSuccess)
            {
                return Result<CreateResponse>
                    .Fail(duplicateExists.Error);
            }
        }

        var advert = AdvertisementCreater
            .Create(request, userId);

        Result<bool> createVariant = null!;

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _advertRepository.Add(advert);

            await _unitOfWork.SaveChangesAsync();

            createVariant = await CreateVariantsAsync(
                advert.Id, 
                request.Variants);

            if (!createVariant.IsSuccess)
                throw new Exception("Failed create Variant.");

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

            if(!createVariant.IsSuccess)
                return Result<CreateResponse>
                    .Fail(createVariant.Error);

            return Result<CreateResponse>
                    .Fail(EntityErrorResources.AdvertisementCreateFailed);
        }

        _logger.LogInformation(
            "Advertisement created successfully. Id: {Id}",
            advert.Id);

        return Result<CreateResponse>.Success(
            new CreateResponse(
                advert.Id,
                advert.CreatedAt
        ));
    }

    private async Task<Result<bool>> CreateVariantsAsync(
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
                return Result<bool>.Fail(result.Error);
        }

        return Result<bool>.Success(true);
    }
}