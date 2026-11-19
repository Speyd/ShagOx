using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Infrastructure.Persistence.DbContexts;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base;
public abstract class RepositoryContext<TEntity>
{
    protected readonly BaseAppDbContext _db;
    protected readonly IUserContext _userContext;

    protected RepositoryContext(
        BaseAppDbContext db,
        IUserContext userContext)
    {
        _db = db;
        _userContext = userContext;
    }

    protected virtual IQueryable<TEntity> ApplyIncludes(
        IQueryable<TEntity> query)
    {
        return query;
    }
    
    protected virtual IQueryable<TEntity> ApplyShortIncludes(
        IQueryable<TEntity> query)
    {
        return query;
    }
}
