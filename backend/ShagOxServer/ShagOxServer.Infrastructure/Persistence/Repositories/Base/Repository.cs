using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Base;
using ShagOxServer.Infrastructure.Persistence.DbContexts;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base;
public  class Repository<T>
    : RepositoryContext, IRepository<T>
    where T : BaseEntity
{
    public Repository(AppDbContext db)
        : base(db)
    {
    }


    public virtual async Task<T?> GetByIdAsync(
        int id)
    {
        return await _db.Set<T>()
            .FindAsync(id);
    }

    public virtual void Add(
        T entity)
    {
        _db.Set<T>()
            .Add(entity);
    }

    public virtual void Delete(
        T entity)
    {
        _db.Set<T>()
            .Remove(entity);
    }

    public virtual bool Update(
        T entity)
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