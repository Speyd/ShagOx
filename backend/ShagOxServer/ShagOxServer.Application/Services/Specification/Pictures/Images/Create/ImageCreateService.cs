using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Specification.Pictures.Create;
using ShagOxServer.Application.DTOs.Specification.Pictures.Images.Create;
using ShagOxServer.Application.DTOs.Specification.Pictures.Images.Create.File;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Create;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Validator;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Images.Create;
public class ImageCreateService 
    : IImageCreateService
{
    private readonly IRepository<Image> _imageRepository;
    private readonly IPictureLoaderService _loaderService;

    private readonly PictureValidator _pictureValidator;

    private readonly AdvertisementValidator _advertValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ImageCreateService> _logger;


    public ImageCreateService(
        IRepository<Image> imageRepository,
        IPictureLoaderService loaderService,
        PictureValidator pictureValidator,
        AdvertisementValidator advertValidator,
        IUnitOfWork unitOfWork,
        ILogger<ImageCreateService> logger)
    {
        _imageRepository = imageRepository;
        _loaderService = loaderService;
        _pictureValidator = pictureValidator;
        _advertValidator = advertValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<PictureCreateResponse>> CreateAsync(
        ImageCreateRequest request)
    {
        var resultValid = await _advertValidator
            .ExistsByIdAsync(request.AdvertisementId);

        if (!resultValid.IsSuccess)
        {
            return Result<PictureCreateResponse>
                .Fail(resultValid.Error);
        }

        var image = ImageCreater.Create(request);

        await _unitOfWork.BeginTransactionAsync();

        try 
        {
            _imageRepository.Add(image);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
               ex,
               "Failed to create image. " + 
               "AdvertisementId: {AdvertisementId}",
               request.AdvertisementId);

            return Result<PictureCreateResponse>
                .Fail("Failed to create image.");
        }

        _logger.LogInformation(
            "Image created successfully. " + 
            "ImageId: {ImageId}, AdvertisementId: {AdvertisementId}",
            image.Id,
            request.AdvertisementId);

        return Success(image);
    }

    public async Task<Result<PictureCreateResponse>> CreateFromFileAsync(
        ImageFileCreateRequest request)
    {
        var resultAdvertValid = await _advertValidator
            .ExistsByIdAsync(request.AdvertisementId);

        if (!resultAdvertValid.IsSuccess)
        {
            return Result<PictureCreateResponse>
                .Fail(resultAdvertValid.Error);
        }

        var resultLoaderValid = await _pictureValidator
                .PictureUploadValidator(request.File);


        await _unitOfWork.BeginTransactionAsync();

        try
        {
            if (!resultLoaderValid.IsSuccess)
            {
                if (resultLoaderValid.Value is not null)
                {
                    await _loaderService
                        .DeleteAsync(resultLoaderValid.Value.PublicId);
                }

                return Result<PictureCreateResponse>
                    .Fail(resultLoaderValid.Error);
            }


            var image = ImageCreater.Create(
                request,
                resultLoaderValid.Value!
            );

            _imageRepository.Add(image);

            await _unitOfWork.CommitAsync();

            _logger.LogInformation(
                "Image created successfully. " +
                "ImageId: {ImageId}, AdvertisementId: {AdvertisementId}",
                image.Id,
                request.AdvertisementId);

            return Success(image);
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            if (resultLoaderValid.Value is not null)
            {
                await _loaderService
                    .DeleteAsync(resultLoaderValid.Value.PublicId);
            }

            _logger.LogError(
               ex,
               "Failed to create image. " +
               "AdvertisementId: {AdvertisementId}",
               request.AdvertisementId);

            return Result<PictureCreateResponse>
                .Fail("Failed to create image.");
        }  
    }

    public async Task<Result<PictureCreateResponse>> CreateFromFileAsync(
        Advertisement advertisement,
        ImageFileCreateRequest request)
    {
        if (advertisement.Id != request.AdvertisementId)
            return Result<PictureCreateResponse>.NotFound("Advertisement");

        return await CreateFromFileAsync(request);
    }

    public async Task<Result<ImagesCreateResponse>> CreateFromFilesAsync(
        ImageFilesCreateRequest request)
    {
        var uploadedImages = new List<string>();

        int order = 0;
        foreach (var file in request.Files)
        {
            var result = await CreateFromFileInternalAsync(
                new ImageFileCreateRequest(
                    file,
                    request.AdvertisementId,
                    order++));


            if (!result.IsSuccess)
            {
                foreach (var publicId in uploadedImages)
                {
                    await _loaderService.DeleteAsync(publicId);
                }

                return Result<ImagesCreateResponse>
                    .Fail(result.Error!);
            }


            uploadedImages.Add(result.Value!.PublicId);
        }

        return Result<ImagesCreateResponse>.Success(
            new ImagesCreateResponse(
                uploadedImages.Count,
                DateTime.UtcNow
        ));
    }

    public async Task<Result<PictureCreateResponse>> CreateFromFileInternalAsync(
        ImageFileCreateRequest request)
    {
        var resultLoaderValid = await _pictureValidator.
            PictureUploadValidator(request.File);

        if (!resultLoaderValid.IsSuccess)
        {
            if (resultLoaderValid.Value is not null)
            {
                await _loaderService
                    .DeleteAsync(resultLoaderValid.Value.PublicId);
            }

            return Result<PictureCreateResponse>
                .Fail(resultLoaderValid.Error);
        }

        var image = ImageCreater.Create(
            request,
            resultLoaderValid.Value!
        );

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _imageRepository.Add(image);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            if (resultLoaderValid.Value is not null)
            {
                await _loaderService
                    .DeleteAsync(resultLoaderValid.Value.PublicId);
            }

            _logger.LogError(
               ex,
               "Failed to create image. " +
               "AdvertisementId: {AdvertisementId}",
               request.AdvertisementId);

            return Result<PictureCreateResponse>
                .Fail("Failed to create image.");
        }

        _logger.LogInformation(
            "Image created successfully. " +
            "ImageId: {ImageId}, AdvertisementId: {AdvertisementId}",
            image.Id,
            request.AdvertisementId);

        return Success(image);
    }

    private static Result<PictureCreateResponse> Success(
        Image image)
    {
        return Result<PictureCreateResponse>.Success(
            new PictureCreateResponse(
                image.Id,
                image.PublicId,
                DateTime.UtcNow
        ));
    }
}