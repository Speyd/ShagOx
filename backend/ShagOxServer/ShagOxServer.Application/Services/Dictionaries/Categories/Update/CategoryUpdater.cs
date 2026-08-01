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

        if (request.Name is not null)
        {
            category.Name = request.Name;
            countUpdated++;
        }

        if (request.ProductTypeId is not null)
        {
            category.ProductTypeId = request.ProductTypeId.Value;
            countUpdated++;
        }

        return countUpdated;
    }
}