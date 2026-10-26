namespace ShagOxServer.Domain.Base.Owner;
public abstract class OwnedEntity
    : BaseEntity
{
    public long OwnerId { get; set; }
}
