using ShagOxServer.Application.DTOs.Auth.Users.UserSessions.Query;
using ShagOxServer.Application.Interfaces.Services.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Filters.Auth.Users.UserSessions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.UserSessions.Query;
public interface IUserSessionQueryService
    : IQueryService<UserSessionDto,
        UserSession,
        UserSessionSearchFilter>
{
    Task<Result<PagedResult<UserSessionDto>>> GetByUserAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<Result<PagedResult<UserSessionDto>>> GetByUserDeviceAsync(
        long deviceId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<Result<PagedResult<UserSessionDto>>> GetByIpAddressAsync(
        string ipAddress,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);
}
