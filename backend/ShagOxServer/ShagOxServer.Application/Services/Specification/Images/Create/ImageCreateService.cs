using ShagOxServer.Application.DTOs.Specification.Images.Create;
using ShagOxServer.Application.DTOs.Specification.Images.Create.File;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Images;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Images.Create;
using ShagOxServer.Application.Services.Advertisements.Validator;
using ShagOxServer.Application.Services.Specification.Images.Create.Validator;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Images.Create;
public class ImageCreateService : IImageCreateService
{
    private readonly IImageRepository _imageRepository;
    private readonly IImageLoaderService _loaderService;
    private readonly ImageCreateValidator _imageValidator;
    private readonly AdvertisementValidator _advertValidator;


    public ImageCreateService(
        IImageRepository imageRepository,
        IImageLoaderService loaderService,
        ImageCreateValidator imageValidator,
        AdvertisementValidator advertValidator)
    {
        _imageRepository = imageRepository;
        _loaderService = loaderService;
        _imageValidator = imageValidator;
        _advertValidator = advertValidator;
    }


    public async Task<Result<ImageCreateResponse>> CreateAsync(
        ImageCreateRequest request)
    {
        var resultValid = await _advertValidator.ExistsByIdAsync(request.AdvertisementId);
        if (!resultValid.IsSuccess)
            return Result<ImageCreateResponse>.Fail(resultValid.Error ?? "");

        var image = ImageCreater.CreateImage(request);

        _imageRepository.Add(image);

        return Success(image);
    }

    public async Task<Result<ImageCreateResponse>> CreateFromFileAsync(
        ImageFileCreateRequest request)
    {
        var resultAdvertValid = await _advertValidator.ExistsByIdAsync(request.AdvertisementId);
        if (!resultAdvertValid.IsSuccess)
            return Result<ImageCreateResponse>.Fail(resultAdvertValid.Error ?? "");

        var resultLoaderValid = await _imageValidator.ImageUploadValidator(request.File);
        if (!resultLoaderValid.IsSuccess)
            return Result<ImageCreateResponse>.Fail(resultLoaderValid.Error ?? "");

        var image = ImageCreater.CreateImage(request, resultLoaderValid.Value!);
        _imageRepository.Add(image);

        return Success(image);
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

    public async Task<Result<ImageCreateResponse>> CreateFromFileInternalAsync(
        ImageFileCreateRequest request)
    {
        var resultLoaderValid = await _imageValidator.ImageUploadValidator(request.File);
        if (!resultLoaderValid.IsSuccess)
            return Result<ImageCreateResponse>.Fail(resultLoaderValid.Error ?? "");

        var image = ImageCreater.CreateImage(
            request,
            resultLoaderValid.Value!);

        _imageRepository.Add(image);

        return Success(image);
    }

    private static Result<ImageCreateResponse> Success(Image image)
    {
        return Result<ImageCreateResponse>.Success(
            new ImageCreateResponse(
                image.Id,
                image.PublicId,
                DateTime.UtcNow));
    }
}