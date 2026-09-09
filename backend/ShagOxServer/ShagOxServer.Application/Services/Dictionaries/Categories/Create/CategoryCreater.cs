using ShagOxServer.Application.DTOs.Dictionaries.Categories.Create;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Create;
public static class CategoryCreater
{
    public static Category Create(
        CategoryCreateRequest request)
    {
        return new Category
        {
            Code = request.Code,
            ProductTypeId = request.ProductTypeId
        };
    }
}