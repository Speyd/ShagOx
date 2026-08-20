using ShagOxServer.Application.DTOs.Specification.Pictures.Create;
using ShagOxServer.Application.DTOs.Specification.Pictures.Images.Create;
using ShagOxServer.Application.DTOs.Specification.Pictures.Images.Create.File;
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


    public ImageCreateService(
        IRepository<Image> imageRepository,
        IPictureLoaderService loaderService,
        PictureValidator pictureValidator,
        AdvertisementValidator advertValidator)
    {
        _imageRepository = imageRepository;
        _loaderService = loaderService;
        _pictureValidator = pictureValidator;
        _advertValidator = advertValidator;
    }


    public async Task<Result<PictureCreateResponse>> CreateAsync(
        ImageCreateRequest request)
    {
        var resultValid = await _advertValidator
            .ExistsByIdAsync(request.AdvertisementId);

        if (!resultValid.IsSuccess)
            return Result<PictureCreateResponse>.Fail(resultValid.Error);

        var image = ImageCreater.Create(request);

        _imageRepository.Add(image);

        return Success(image);
    }

    public async Task<Result<PictureCreateResponse>> CreateFromFileAsync(
        ImageFileCreateRequest request)
    {
        var resultAdvertValid = await _advertValidator
            .ExistsByIdAsync(request.AdvertisementId);

        if (!resultAdvertValid.IsSuccess)
            return Result<PictureCreateResponse>.Fail(resultAdvertValid.Error);


        var resultLoaderValid = await _pictureValidator
            .PictureUploadValidator(request.File);

        if (!resultLoaderValid.IsSuccess)
            return Result<PictureCreateResponse>.Fail(resultLoaderValid.Error);


        var image = ImageCreater.Create(
            request, 
            resultLoaderValid.Value!
        );

        _imageRepository.Add(image);

        return Success(image);
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
            return Result<PictureCreateResponse>
                .Fail(resultLoaderValid.Error);

        var image = ImageCreater.Create(
            request,
            resultLoaderValid.Value!
        );

        _imageRepository.Add(image);

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