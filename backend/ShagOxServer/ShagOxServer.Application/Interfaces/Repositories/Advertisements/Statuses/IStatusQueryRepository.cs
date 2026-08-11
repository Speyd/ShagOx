using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses;
public interface IStatusQueryRepository
    : IQueryRepository<Status>
{
    Task<PagedResult<Status>> Search(
       StatusSearchFilter filter,
       PaginationParams pagination);
}