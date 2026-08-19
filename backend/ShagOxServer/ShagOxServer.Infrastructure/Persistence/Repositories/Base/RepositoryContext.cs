using ShagOxServer.Infrastructure.Persistence.DbContexts;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base;
public abstract class RepositoryContext
{
    protected readonly AppDbContext _db;

    protected RepositoryContext(AppDbContext db)
    {
        _db = db;
    }
}