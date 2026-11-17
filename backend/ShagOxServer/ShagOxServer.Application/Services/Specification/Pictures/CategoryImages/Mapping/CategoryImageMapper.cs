using ShagOxServer.Application.DTOs.Specification.Pictures.CategoryImages.Query;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Specification.Pictures.CategoryImages.Mapping;
public static class CategoryImageQueryMapper
{
    public static CategoryImageDto ToDto(
        CategoryImage categoryImage)
    {
        return new CategoryImageDto(
            categoryImage.Id,
            categoryImage.EntityStatus,
            categoryImage.Url,
            categoryImage.PublicId,
            categoryImage.OwnerId
        );
    }
}
