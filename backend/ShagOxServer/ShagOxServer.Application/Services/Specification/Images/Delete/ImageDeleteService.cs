using ShagOxServer.Application.DTOs.Specification.Images.Delete;
using ShagOxServer.Application.Interfaces.Specification.Images.Delete;
using ShagOxServer.Infrastructure.Interfaces.Specification.Images;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Images.Delete;
public class ImageDeleteService : IImageDeleteService
{
    private readonly IImageRepository _repository;

    public ImageDeleteService(
        IImageRepository imageRepository)
    {
        _repository = imageRepository;
    }

    public async Task<Result<ImageDeleteResponse>> DeleteImageAsync(int id)
    {
        var image = await _repository.GetByIdAsync(id);
        if (image is null)
            return Result<ImageDeleteResponse>.NotFound("Image");

        await _repository.DeleteAsync(image);
        return Result<ImageDeleteResponse>.Success(
           new ImageDeleteResponse(
               image.Id,
               DateTime.UtcNow
           )
       );
    }
}
