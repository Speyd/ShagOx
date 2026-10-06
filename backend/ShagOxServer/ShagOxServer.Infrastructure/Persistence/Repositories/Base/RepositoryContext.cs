using ShagOxServer.Infrastructure.Persistence.DbContexts;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base;
public abstract class RepositoryContext<TEntity>
{
    protected readonly BaseAppDbContext _db;

    protected RepositoryContext(BaseAppDbContext db)
    {
        _db = db;
    }

    protected virtual IQueryable<TEntity> ApplyIncludes(
        IQueryable<TEntity> query)
    {
        return query;
    }
}