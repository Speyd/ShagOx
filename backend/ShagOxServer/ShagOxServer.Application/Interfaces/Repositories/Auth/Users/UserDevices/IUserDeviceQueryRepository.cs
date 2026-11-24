using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Filters.Auth.Users.UserDevices;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices;
public partial interface IUserDeviceQueryRepository
    : ISearchRepository<UserDevice, UserDeviceSearchFilter>
{
    Task<PagedResult<UserDevice>> GetByUserAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<List<UserDevice>> GetAllByUserAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<UserDevice?> GetByIdentifierAsync(
        string deviceIdentifier,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<PagedResult<UserDevice>> GetByIpAsync(
        string ipAddress,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
