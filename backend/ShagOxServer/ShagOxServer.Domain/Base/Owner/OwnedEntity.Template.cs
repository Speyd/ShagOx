namespace ShagOxServer.Domain.Base.Owner;
public abstract class OwnedEntity<TOwner>
    : OwnedEntity
{
    public TOwner Owner { get; set; } = default(TOwner)!;
}
