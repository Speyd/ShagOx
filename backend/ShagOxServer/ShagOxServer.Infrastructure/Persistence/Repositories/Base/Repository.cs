using ShagOxServer.Application.Interfaces.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base;
public  class Repository<T>
    : RepositoryContext, IRepository<T>
    where T : class
{
    public Repository(AppDbContext db)
        : base(db)
    {
    }


    public async Task<T?> GetByIdAsync(int id)
    {
        return await _db.Set<T>()
            .FindAsync(id);
    }

    public void Add(T entity)
    {
        _db.Set<T>()
            .Add(entity);
    }

    public void Delete(T entity)
    {
        _db.Set<T>()
            .Remove(entity);
    }

    public bool Update(T entity)
    {
        try
        {
            _db.Set<T>()
                .Update(entity);

            return true;
        }
        catch
        {
            return false;
        }     
    }
}