using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Cache;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Services.Dictionaries.ProductTypes.Mapping;
public static class ProductTypeCacheMapper
{
    public static ProductTypeCacheInfo ToInfo(
       ProductType productType,
       EntityStatus status = EntityStatus.Active)
    {
        return new ProductTypeCacheInfo(
            productType.Id,
            status
        );
    }
}
