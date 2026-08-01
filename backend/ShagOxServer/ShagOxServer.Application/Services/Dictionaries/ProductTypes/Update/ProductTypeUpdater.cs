using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Update;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Services.Dictionaries.ProductTypes.Update;
public static class ProductTypeUpdater
{
    public static int ApplyUpdates(
       ProductType productType,
       ProductTypeUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.Name is not null)
        {
            productType.Name = request.Name;
            countUpdated++;
        }

        if (request.Description is not null)
        {
            productType.Description = request.Description;
            countUpdated++;
        }

        return countUpdated;
    }
}