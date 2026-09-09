using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Services.Dictionaries.ProductTypes.Mapping;
public static class ProductTypeMapper
{
    public static ProductTypeDto ToDto(
       ProductType productType)
    {
        return new ProductTypeDto(
            productType.Id,
            productType.Code,
            productType.Description
        );
    }
}