using ShagOxServer.Application.DTOs.Dictionaries.Categories.Cache;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Mapping;
public static class CategoryCacheMapper
{
    public static CategoryCacheInfo ToInfo(
       Category category)
    {
        return new CategoryCacheInfo(
            category.Id,
            category.ProductTypeId
        );
    }
}
