using ShagOxServer.Application.DTOs.Specification.Images;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Images;
using ShagOxServer.Application.Interfaces.Services.Specification.Images.Query;
using ShagOxServer.Application.Services.Specification.Images.Mapping;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Specification.Images.Query;
public class ImageQueryService : IImageQueryService
{
    private readonly IImageRepository _imageRepository;


    public ImageQueryService(
        IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }


    public async Task<Result<ImageDto>> GetByIdAsync(int id)
    {
        var image = await _imageRepository
            .GetByIdAsync(id);

        return image.ToResult(ImageMapper.ToDto);
    }
}