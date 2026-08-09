using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses;
public interface IStatusQueryRepository
{
    Task<Status?> GetByIdAsync(int id);
}