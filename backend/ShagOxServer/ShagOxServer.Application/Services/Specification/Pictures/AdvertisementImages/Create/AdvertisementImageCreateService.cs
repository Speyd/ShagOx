using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Response;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Mapping;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Images.Create;
public partial class AdvertisementImageCreateService 
    : IAdvertisementImageCreateService
{
    private readonly IRepository<AdvertisementImage> _imageRepository;
    private readonly IPictureLoaderService _loaderService;

    private readonly PictureValidator _pictureValidator;
    private readonly ImageInvalidationService _imageInvalidation;

    private readonly AdvertisementValidator _advertValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AdvertisementImageCreateService> _logger;


    public AdvertisementImageCreateService(
        IRepository<AdvertisementImage> imageRepository,
        IPictureLoaderService loaderService,
        PictureValidator pictureValidator,
        AdvertisementValidator advertValidator,
        ImageInvalidationService imageInvalidation,
        IUnitOfWork unitOfWork,
        ILogger<AdvertisementImageCreateService> logger)
    {
        _imageRepository = imageRepository;
        _loaderService = loaderService;
        _pictureValidator = pictureValidator;
        _advertValidator = advertValidator;
        _imageInvalidation = imageInvalidation;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<PictureCreateResponse>> CreateAsync(
        PictureOrderCreateRequest request)
    {
        var ownerValid = await _advertValidator
            .ExistsByIdAsync(request.OwnerId);

        if (!ownerValid.IsSuccess)
        {
            return Result<PictureCreateResponse>
                .Fail(ownerValid.Error);
        }

       

        await _unitOfWork.BeginTransactionAsync();

        var resultLoaderValid = await _pictureValidator
           .PictureUploadValidator(request.File);

        try
        {
            if (!resultLoaderValid.IsSuccess)
                throw new Exception("Fail image upload");

            var image = AdvertisementImageCreater.Create(
                request,
                resultLoaderValid.Value!
            );

            _imageRepository.Add(image);

            await _unitOfWork.CommitAsync();

            return await Success(image);
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();        

            _logger.LogError(
               ex,
               "Failed to create image. " +
               "AdvertisementId: {AdvertisementId}",
               request.OwnerId);

            if (resultLoaderValid.Value?.PublicId is not null)
            {
                await _loaderService
                    .DeleteAsync(resultLoaderValid.Value.PublicId);

                return Result<PictureCreateResponse>
                    .Fail(resultLoaderValid.Error);
            }

            return Result<PictureCreateResponse>
                .Fail(EntityErrorResources.ImageCreateFailed);
        }  
    }

    private async Task<Result<PictureCreateResponse>> Success(
        AdvertisementImage image)
    {
        _logger.LogInformation(
            "Image created successfully. " +
            "ImageId: {ImageId}, AdvertisementId: {AdvertisementId}",
            image.Id,
            image.OwnerId);

        await _imageInvalidation.InvalidateCreateAsync(
            AdvertisementImageCacheMapper.ToInfo(image));

        return Result<PictureCreateResponse>.Success(
            new PictureCreateResponse(
                image.Id,
                image.PublicId,
                DateTime.UtcNow
        ));
    }

    //public async Task<Result<PictureCreateResponse>> CreateFromFileAsync(
    //    Advertisement advertisement,
    //    PictureOrderCreateRequest request)
    //{
    //    if (advertisement.Id != request.OwnerId)
    //    {
    //        return Result<PictureCreateResponse>
    //            .NotFound(EntityNamesResources.Advertisement);
    //    }

    //    return await CreateFromFileAsync(request);
    //}

}
