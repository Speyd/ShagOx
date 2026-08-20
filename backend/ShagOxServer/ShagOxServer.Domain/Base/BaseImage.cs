namespace ShagOxServer.Domain.Base;
public abstract class BaseImage : BaseEntity
{
    public string Url { get; set; } = null!;
    public string PublicId { get; set; } = null!;
}