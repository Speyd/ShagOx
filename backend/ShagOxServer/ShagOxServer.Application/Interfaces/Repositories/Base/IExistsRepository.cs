using ShagOxServer.Domain.Base;

namespace ShagOxServer.Application.Interfaces.Repositories.Base;
public interface IExistsRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<bool> ExistsByIdAsync(
        long id);
}