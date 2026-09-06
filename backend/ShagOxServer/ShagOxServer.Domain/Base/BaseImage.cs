namespace ShagOxServer.Domain.Base;
public abstract class BaseImage 
    : BaseEntity
{
    public string Url { get; set; } = "";
    public string PublicId { get; set; } = "";
}