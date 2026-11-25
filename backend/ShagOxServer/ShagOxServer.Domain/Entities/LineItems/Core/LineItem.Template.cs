using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Cache.LineItems;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Domain.Entities.LineItems.Core;
public abstract class LineItem<TParent>
    : LineItem
    where TParent : BaseEntity
{
    public TParent Parent { get; set; } = null!;
}
