using ShagOxServer.Application.DTOs.Dictionaries.Categories;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Mapping;
public static class CategoryMapper
{
    public static CategoryDto ToDto(
       Category category)
    {
        return new CategoryDto(
            category.Id,
            category.Name,
            category.ProductType,
            category.Attributes.Select(x => x.Id).ToList(),
            category.Advertisements.Select(x => x.Id).ToList()
        );
    }
}
