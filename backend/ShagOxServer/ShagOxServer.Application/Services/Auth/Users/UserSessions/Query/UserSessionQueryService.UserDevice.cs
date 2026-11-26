using ShagOxServer.Application.DTOs.Auth.Users.UserSessions.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserSessions.Query;
using ShagOxServer.Application.Services.Base.Query;
using ShagOxServer.Application.Services.Caches.Keys.Auth;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Filters.Auth.Users.UserSessions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Auth.Users.UserSessions.Query;
public partial class UserSessionQueryService
    : BaseQueryService<
        UserSessionDto,
        UserSession,
        UserSessionSearchFilter
        >,
    IUserSessionQueryService
{
    public async Task<Result<PagedResult<UserSessionDto>>> GetByUserDeviceAsync(
        long deviceId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = UserSessionCache.ByUserDevice(
            deviceId,
            pagination.Page,
            pagination.PageSize,
            status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var sessions = await _sessionQueryRepository
                    .GetByUserDeviceAsync(deviceId, pagination,
                        cancellationToken, status);

                return await sessions.ToResultPagedAsync(
                    ApplyMapperAsync);
            },
            _cacheOptions.DefaultExpiration
        );
    }

    public async Task<Result<List<UserSessionDto>>> GetAllByUserDeviceAsync(
        long deviceId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = UserSessionCache.AllByUserDevice(
            deviceId,
            status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var sessions = await _sessionQueryRepository
                    .GetAllByUserDeviceAsync(deviceId,
                        cancellationToken, status);

                return await sessions.ToResultListAsync(
                    ApplyMapperAsync);
            },
            _cacheOptions.DefaultExpiration
        );
    }
}
