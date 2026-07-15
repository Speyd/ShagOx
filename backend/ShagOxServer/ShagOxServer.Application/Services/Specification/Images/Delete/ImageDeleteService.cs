using ShagOxServer.Application.DTOs.Specification.Images.Delete;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Images;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Application.Interfaces.Services.Specification.Images.Delete;
using ShagOxServer.Application.Services.Specification.Images.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Images.Delete;
public class ImageDeleteService : IImageDeleteService
{
    private readonly IImageRepository _imageRepository;
    private readonly ImageValidator _imageValidator;
    private readonly IImageLoaderService _loaderService;


    public ImageDeleteService(
        IImageRepository imageRepository,
        ImageValidator imageValidator,
        IImageLoaderService loaderService)
    {
        _imageRepository = imageRepository;
        _imageValidator = imageValidator;
        _loaderService = loaderService;
    }


    public async Task<Result<ImageDeleteResponse>> DeleteAsync(int id)
    {
        var image = await _imageValidator.GetByIdAsync(id);
        if (!image.IsSuccess)
            return Result<ImageDeleteResponse>.Fail(image.Error ?? "");

        var result = await _loaderService.DeleteAsync(image.Value!.PublicId);

        if (!result.IsSuccess)
            return Result<ImageDeleteResponse>.Fail(result.Error!);

        _imageRepository.Delete(image.Value!);

        return Result<ImageDeleteResponse>.Success(
           new ImageDeleteResponse(
               image.Value!.Id,
               DateTime.UtcNow
           )
       );
    }

    public async Task<Result<ImageDeleteResponse>> DeleteRecordAsync(int id)
    {
        var image = await _imageValidator.GetByIdAsync(id);
        if (!image.IsSuccess)
            return Result<ImageDeleteResponse>.Fail(image.Error ?? "");

        _imageRepository.Delete(image.Value!);

        return Result<ImageDeleteResponse>.Success(
           new ImageDeleteResponse(
               image.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}