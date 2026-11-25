namespace ShagOxServer.Domain.Base.Cache;

public interface IEntityCache<TEntity, TContext> 
    : IEntityCache
    where TEntity : BaseEntity
{
    new TContext GetCacheContext();
}
