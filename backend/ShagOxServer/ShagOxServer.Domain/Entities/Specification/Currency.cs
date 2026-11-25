using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Cache.Specification;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Domain.Entities.Specification;
public class Currency 
    : BaseEntity,
    IEntityCache<Currency, CurrencyCacheContext>
{
    public string Code { get; set; } = "";
    public int NumericCode { get; set; }
    public int DecimalPlaces { get; set; }
    public string Symbol { get; set; } = "";
    public string Name { get; set; } = "";

    public ICollection<User> Users { get; set; }
        = [];


    public CurrencyCacheContext GetCacheContext()
    {
        return new CurrencyCacheContext(
            Id, Code, Symbol, EntityStatus);
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
