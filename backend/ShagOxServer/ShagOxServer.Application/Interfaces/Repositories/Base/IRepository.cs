using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.Interfaces.Repositories.Base;
public interface IRepository<T>
     where T : BaseEntity
{
    Task<T?> GetByIdAsync(
        long entityId, 
        CancellationToken cancellationToken = default, 
        EntityStatus? entityStatus = null);

    void Attach(T entity);

    void Add(T entity);

    void Delete(T entity);

    bool Update(T entity);
}
