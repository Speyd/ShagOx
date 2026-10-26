namespace ShagOxServer.Domain.Base.Pictures;
public abstract class BasePicture<TOwner>
    : BasePicture
    where TOwner : BaseEntity
{
    public TOwner Owner { get; set; } = null!;
}
