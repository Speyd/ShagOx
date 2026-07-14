using ShagOxServer.Application.Interfaces.Repositories.Specification.Images;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Images.Validator;
public class ImageValidator
{
    private readonly IImageRepository _imageRepository;


    public ImageValidator(
        IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }


    public async Task<Result<Image>> GetByIdAsync(
        int imageId)
    {
        var image = await _imageRepository.GetByIdAsync(imageId);

        if (image is null)
            return Result<Image>.NotFound("Image");

        return Result<Image>.Success(image);
    }
}