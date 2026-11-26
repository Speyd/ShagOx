using ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Services.Base.Query;
using ShagOxServer.Application.Services.Caches.Keys.Auth;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Filters.Auth.Users.UserDevices;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Auth.Users.UserDevices.Query;
public partial class UserDeviceQueryService
    : BaseQueryService<
        UserDeviceDto,
        UserDevice,
        UserDeviceSearchFilter
        >,
    IUserDeviceQueryService
{
    public async Task<Result<PagedResult<UserDeviceDto>>> GetByUserAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = UserDeviceCache.ByUser(
          userId,
          pagination.Page,
          pagination.PageSize,
          status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var devices = await _userDeviceQueryRepository
                    .GetByUserAsync(userId, pagination,
                        cancellationToken, status);

                return await devices.ToResultPagedAsync(
                    ApplyMapperAsync);
            },
            _cacheOptions.DefaultExpiration
        );
    }

    public async Task<Result<List<UserDeviceDto>>> GetAllByUserAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = UserDeviceCache.AllByUser(
          userId,
          status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var devices = await _userDeviceQueryRepository
                    .GetAllByUserAsync(userId,
                        cancellationToken, status);

                return await devices.ToResultListAsync(
                    ApplyMapperAsync);
            },
            _cacheOptions.DefaultExpiration
        );
    }
}
