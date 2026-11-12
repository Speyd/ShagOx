using ShagOxServer.Application.DTOs.LineItems.LineItemAttributes.Cache;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.LineItems;

namespace ShagOxServer.Application.Services.LineItems.LineItemAttributes.Mapping;
public static class LineItemAttributeCacheMapper
{
    public static LineItemAttributeCacheInfo ToInfo(
        LineItemAttribute attribute,
        long categoryId,
        EntityStatus status = EntityStatus.Active)
    {
        return new LineItemAttributeCacheInfo(
            attribute.Id,
            categoryId,
            attribute.AttributeDefinitionId,
            status
        );
    }
}
