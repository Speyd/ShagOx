namespace ShagOxServer.Infrastructure.Persistence.Repositories;
public abstract class RepositoryContext
{
    protected readonly AppDbContext _db;

    protected RepositoryContext(AppDbContext db)
    {
        _db = db;
    }
}