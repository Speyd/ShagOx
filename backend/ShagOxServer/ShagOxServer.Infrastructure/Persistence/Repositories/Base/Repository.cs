using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;

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
        long entityId,
        CancellationToken cancellationToken = default,
        EntityStatus? entityStatus = null)
    {
        return await _db.Set<TEntity>()
            .WithStatusFilter(entityStatus)
            .FirstOrDefaultAsync(x => x.Id == entityId,
                cancellationToken);
    }

    public virtual void Attach(
        TEntity entity)
    {
        _db.Set<TEntity>().Attach(entity);
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
