using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Base;
using ShagOxServer.Infrastructure.Persistence.DbContexts;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base;
public  class Repository<TEntity>
    : RepositoryContext<TEntity>, 
    IRepository<TEntity>
    where TEntity : BaseEntity
{
    public Repository(AppDbContext db)
        : base(db)
    {
    }


    public virtual async Task<TEntity?> GetByIdAsync(
        long id)
    {
        return await _db.Set<TEntity>()
            .FindAsync(id);
    }

    public virtual void Add(
        TEntity entity)
    {
        _db.Set<TEntity>()
            .Add(entity);
    }

    public virtual void Delete(
        TEntity entity)
    {
        _db.Set<TEntity>()
            .Remove(entity);
    }

    public virtual bool Update(
        TEntity entity)
    {
        try
        {
            _db.Set<TEntity>()
                .Update(entity);

            return true;
        }
        catch
        {
            return false;
        }     
    }
}