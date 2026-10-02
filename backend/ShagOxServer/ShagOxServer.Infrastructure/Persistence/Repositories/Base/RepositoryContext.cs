using ShagOxServer.Infrastructure.Persistence.DbContexts;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base;
public abstract class RepositoryContext<TEntity>
{
    protected readonly AppDbContext _db;

    protected RepositoryContext(AppDbContext db)
    {
        _db = db;
    }

    protected virtual IQueryable<TEntity> ApplyIncludes(
        IQueryable<TEntity> query)
    {
        return query;
    }
}