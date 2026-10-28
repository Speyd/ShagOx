using ShagOxServer.Domain.Base;

namespace ShagOxServer.Application.Interfaces.Repositories.Base;
public interface IRepository<T>
     where T : BaseEntity
{
    void Attach(T entity);

    void Add(T entity);

    void Delete(T entity);

    bool Update(T entity);
}
