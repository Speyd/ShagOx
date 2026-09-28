using ShagOxServer.Application.DTOs.Specification.Pictures.Images;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Images;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Query;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Mapping;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.Images;

namespace ShagOxServer.Application.Services.Specification.Pictures.Images.Query;
public class ImageQueryService 
    : BaseQueryService<
        ImageDto,
        Image,
        ImageSearchFilter
        >,
    IImageQueryService
{
    public ImageQueryService(
        IImageQueryRepository imageQueryRepository
    )
        : base(imageQueryRepository)
    {
    }


    protected override async Task<ImageDto> ApplyMapperAsync(Image entity)
    {
        return ImageMapper.ToDto(entity);
    }
}