using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Caches;
using ShagOxServer.Application.DTOs.Auth.Users.UserSessions.Query;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserSessions.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserSessions.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Auth.Users.UserSessions.Mapping;
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
    private readonly IUserSessionQueryRepository _sessionQueryRepository;

    protected override bool CacheBySearch => false;

    protected override bool CacheByPaged => false;


    public UserSessionQueryService(
        IUserSessionQueryRepository sessionQueryRepository,
        IDataCacheService cacheService,
        IOptions<CacheOptions> cacheOptions
    )
        : base(sessionQueryRepository,
            cacheService,
            cacheOptions)
    {
        _sessionQueryRepository = sessionQueryRepository;
    }


    public override async Task<UserSessionDto> ApplyMapperAsync(
        UserSession entity)
    {
        return UserSessionMapper.ToDto(entity);
    }

    public async Task<Result<PagedResult<UserSessionDto>>> GetByIpAddressAsync(
        string ipAddress,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = UserSessionCache.ByIpAddress(
            ipAddress,
            pagination.Page,
            pagination.PageSize,
            status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var sessions = await _sessionQueryRepository
                    .GetByIpAddressAsync(ipAddress, pagination,
                        cancellationToken, status);

                return await sessions.ToResultPagedAsync(
                    ApplyMapperAsync);
            },
            _cacheOptions.DefaultExpiration
        );
    }
}
