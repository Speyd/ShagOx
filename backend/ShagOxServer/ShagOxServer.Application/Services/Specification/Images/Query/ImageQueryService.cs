using ShagOxServer.Application.DTOs.Specification.Images;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Images;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Images.Query;
using ShagOxServer.Application.Services.Specification.Images.Mapping;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Specification.Images.Query;
public class ImageQueryService : IImageQueryService
{
    private readonly IImageRepository _repository;

    public ImageQueryService(
        IImageRepository imageRepository)
    {
        _repository = imageRepository;
    }

    public async Task<Result<ImageDto>> GetByIdAsync(int id)
    {
        var image = await _repository.GetByIdAsync(id);

        return image.ToResult(ImageMapper.ToDto);
    }
}