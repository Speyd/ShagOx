using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Cache.Auth;

namespace ShagOxServer.Domain.Entities.Auth;
public class Role 
    : BaseEntity,
    IEntityCache<Role, RoleCacheContext>
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";

    public ICollection<UserRole> UserRoles { get; set; } 
        = [];


    public RoleCacheContext GetCacheContext()
    {
        return new RoleCacheContext(
            Id, Name, EntityStatus);
    }

    public override string ToString()
    {
        return Name;
    }

    object IEntityCache.GetCacheContext()
    {
        return GetCacheContext();
    }
}
