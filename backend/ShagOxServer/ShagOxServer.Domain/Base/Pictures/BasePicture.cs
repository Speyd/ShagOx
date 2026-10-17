namespace ShagOxServer.Domain.Base.Pictures;
public abstract class BasePicture<TOwner>
    : BaseEntity
    where TOwner : BaseEntity
{
    public string Url { get; set; } = "";
    public string PublicId { get; set; } = "";

    public long OwnerId { get; set; }
    public TOwner Owner { get; set; } = null!;
}
