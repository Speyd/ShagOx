using ShagOxServer.Application.DTOs.Dictionaries.Categories.Update;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Update;
public static class CategoryUpdater
{
    public static int ApplyUpdates(
        Category category,
        CategoryUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.Code is not null)
        {
            category.Code = request.Code;
            countUpdated++;
        }

        if (request.ProductTypeId.HasValue)
        {
            category.ProductTypeId = request.ProductTypeId.Value;
            countUpdated++;
        }

        return countUpdated;
    }
}