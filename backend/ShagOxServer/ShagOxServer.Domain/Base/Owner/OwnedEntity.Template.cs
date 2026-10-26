namespace ShagOxServer.Domain.Base.Owner;
public abstract class OwnedEntity<TOwner>
    : OwnedEntity
    where TOwner: BaseEntity
{
    public TOwner Owner { get; set; } = null!;
}
