namespace ShagOxServer.Application.Interfaces.Repositories.Base;
public interface IRepository<T>
     where T : class
{
    Task<T?> GetByIdAsync(int id);

    void Add(T entity);

    void Delete(T entity);

    bool Update(T entity);
}