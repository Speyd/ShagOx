using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Filters.Auth.Users.UserDevices;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices.Query;
public partial interface IUserDeviceQueryRepository
    : ISearchRepository<UserDevice, UserDeviceSearchFilter>
{
    Task<UserDevice?> GetByUserAndDeviceAsync(
        long userId,
        long deviceId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<UserDevice?> GetByUserAndDeviceNoTrackingAsync(
        long userId,
        long deviceId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<PagedResult<UserDevice>> GetByUserAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<List<UserDevice>> GetAllByUserAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<PagedResult<UserDevice>> GetByDeviceAsync(
        long deviceId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<List<UserDevice>> GetAllByDeviceAsync(
        long deviceId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
