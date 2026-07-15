using ShagOxServer.Application.DTOs.Dictionaries.Categories.Create;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Create;
public static class CategoryCreater
{
    public static Category CreateCategory(
        CategoryCreateRequest request)
    {
        return new Category
        {
            Name = request.Name,
            ProductType = request.ProductType
        };
    }
}