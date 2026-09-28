namespace ShagOxServer.Domain.Base;
public abstract class BaseEntity
{
    public long Id { get; set; }

    public abstract override string ToString();
}