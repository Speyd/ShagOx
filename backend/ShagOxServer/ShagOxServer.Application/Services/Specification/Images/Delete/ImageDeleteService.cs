using ShagOxServer.Application.DTOs.Specification.Images.Delete;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Images;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Images.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Images.Delete;
public class ImageDeleteService : IImageDeleteService
{
    private readonly IImageRepository _repository;
    private readonly IImageLoaderService _loaderService;

    public ImageDeleteService(
        IImageRepository imageRepository,
        IImageLoaderService loaderService)
    {
        _repository = imageRepository;
        _loaderService = loaderService;
    }

    public async Task<Result<ImageDeleteResponse>> DeleteImageAsync(int id)
    {
        var image = await _repository.GetByIdAsync(id);
        if (image is null)
            return Result<ImageDeleteResponse>.NotFound("Image");

        var result = await _loaderService.DeleteAsync(image.PublicId);

        if (!result.IsSuccess)
            return Result<ImageDeleteResponse>.Fail(result.Error!);

        _repository.DeleteAsync(image);

        return Result<ImageDeleteResponse>.Success(
           new ImageDeleteResponse(
               image.Id,
               DateTime.UtcNow
           )
       );
    }
}
