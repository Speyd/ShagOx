namespace ShagOxServer.Application.Interfaces.Repositories.Base;
public interface IExistsRepository<T>
    where T : class
{
    Task<bool> ExistsByIdAsync(int id);
}