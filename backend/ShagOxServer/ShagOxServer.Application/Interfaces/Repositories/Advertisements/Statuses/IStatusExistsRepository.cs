using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses;
public interface IStatusExistsRepository
    : IExistsRepository<Status>
{
    Task<bool> ExistsByCodeAsync(string code);

    Task<bool> ExistsByNameAsync(string name);
}