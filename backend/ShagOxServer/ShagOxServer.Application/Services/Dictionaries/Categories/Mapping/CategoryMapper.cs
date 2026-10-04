using ShagOxServer.Application.DTOs.Dictionaries.Categories;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Query;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Mapping;
public static class CategoryMapper
{
    public static CategoryDto ToDto(
       Category category,
       string? lable)
    {
        return new CategoryDto(
            category.Id,
            category.Code,
            new CategoryProductTypeDto(
                category.ProductType.Id,
                category.ProductType.Code
            ),
            lable
        );
    }
}
