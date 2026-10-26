using ShagOxServer.Domain.Base.Owner;

namespace ShagOxServer.Domain.Base.Pictures;
public abstract class BasePicture
    : OwnedEntity
{
    public string Url { get; set; } = "";
    public string PublicId { get; set; } = "";
}
