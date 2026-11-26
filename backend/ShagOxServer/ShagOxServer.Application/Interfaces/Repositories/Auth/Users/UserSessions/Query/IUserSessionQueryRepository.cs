using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Filters.Auth.Users.UserSessions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserSessions.Query;
public partial interface IUserSessionQueryRepository
    : ISearchRepository<UserSession, UserSessionSearchFilter>
{
    Task<PagedResult<UserSession>> GetByUserAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<List<UserSession>> GetAllByUserAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<PagedResult<UserSession>> GetByUserDeviceAsync(
        long deviceId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<List<UserSession>> GetAllByUserDeviceAsync(
        long deviceId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<UserSession?> GetByRefreshTokenHashAsync(
        string refreshTokenHash,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
