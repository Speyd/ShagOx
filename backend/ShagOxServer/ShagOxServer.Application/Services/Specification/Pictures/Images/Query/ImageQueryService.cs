using ShagOxServer.Application.DTOs.Specification.Pictures.Images;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Images;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Query;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Mapping;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Specification.Pictures.Images.Query;
public class ImageQueryService 
    : IImageQueryService
{
    private readonly IImageQueryRepository _imageQueryRepository;


    public ImageQueryService(
        IImageQueryRepository imageQueryRepository)
    {
        _imageQueryRepository = imageQueryRepository;
    }


    public async Task<Result<ImageDto>> GetByIdAsync(
        int id)
    {
        var image = await _imageQueryRepository
            .GetByIdAsync(id);

        return image.ToResult(ImageMapper.ToDto);
    }

    public async Task<Result<PagedResult<ImageDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var images = await _imageQueryRepository
            .GetPagedAsync(pagination);

        return images.ToResultPaged(ImageMapper.ToDto);
    }
}