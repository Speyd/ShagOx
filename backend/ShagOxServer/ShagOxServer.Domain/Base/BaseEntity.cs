using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Domain.Base;
public abstract class BaseEntity
{
    public long Id { get; set; }

    public EntityStatus EntityStatus { get; set; } 
        = EntityStatus.Active;

    public DateTime? DeletedAt { get; set; }


    public abstract override string ToString();
}
