
namespace ShagOxServer.Infrastructure.Persistence.Repositories;
public abstract class BaseRepository
{
    protected readonly AppDbContext _db;
    public BaseRepository(AppDbContext db)
    {
        _db = db;
    }
}
