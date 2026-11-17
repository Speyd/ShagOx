using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.LineItems.Core;
public abstract class LineItem<TParent>
    : LineItem
    where TParent : BaseEntity
{
    public TParent Parent { get; set; } = null!;
}
