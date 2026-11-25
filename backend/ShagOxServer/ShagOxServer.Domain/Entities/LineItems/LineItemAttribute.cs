using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Cache.LineItems;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Domain.Entities.LineItems;
public class LineItemAttribute
    : BaseEntity,
    IEntityCache<LineItemAttribute,
        LineItemAttributeCacheContext>
{
    public AttributeDefinition AttributeDefinition { get; set; } = null!;
    public long AttributeDefinitionId { get; set; }

    public int Order { get; set; }


    public LineItemAttributeCacheContext GetCacheContext()
    {
        return new LineItemAttributeCacheContext(
            Id, AttributeDefinition.CategoryId, AttributeDefinitionId, EntityStatus);
    }

    public override string ToString()
    {
        return $"Attr.: {AttributeDefinitionId} | Ord.: {Order}";
    }

    object IEntityCache.GetCacheContext()
    {
        return GetCacheContext();
    }
}
