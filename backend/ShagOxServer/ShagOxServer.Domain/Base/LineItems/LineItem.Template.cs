namespace ShagOxServer.Domain.Base.Items;
public abstract class LineItem<TParent>
    : LineItem
    where TParent : BaseEntity
{
    public TParent Parent { get; set; } = null!;
}
