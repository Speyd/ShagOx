namespace ShagOxServer.Application.Interfaces.Repositories.Base;
public interface IExistsRepository<T>
{
    Task<bool> ExistsByIdAsync(int id);
}