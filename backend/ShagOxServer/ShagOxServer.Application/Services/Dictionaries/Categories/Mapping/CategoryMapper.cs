using ShagOxServer.Application.DTOs.Dictionaries.Categories.Query;
using ShagOxServer.Application.DTOs.Specification.Pictures.CategoryImages.Query;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Mapping;
public static class CategoryMapper
{
    public static CategoryDto ToDto(
       Category category,
       CategoryImageDto? imageDto,
       string? label)
    {
        return new CategoryDto(
            category.Id,
            category.EntityStatus,
            category.Code,
            category.ProductTypeId,
            imageDto,
            label
        );
    }
}
