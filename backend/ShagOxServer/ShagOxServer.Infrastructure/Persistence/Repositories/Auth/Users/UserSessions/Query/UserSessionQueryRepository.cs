using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserSessions.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Filters.Auth.Users.UserSessions;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserDevices.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserSessions.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserSessions.Query;
public partial class UserSessionQueryRepository
    : SearchRepository<UserSession, UserSessionSearchFilter>,
      IUserSessionQueryRepository
{
    public UserSessionQueryRepository(
        ReplicaDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }


    public async Task<PagedResult<UserSession>> GetByUserAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken,
        EntityStatus? status = null)
    {
        return await _db.UserSessions
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x => x.OwnerId == userId)
            .ToPagedResultAsync(pagination,
                x => x.Id,
                cancellationToken);
    }

    public async Task<List<UserSession>> GetAllByUserAsync(
        long userId,
        CancellationToken cancellationToken,
        EntityStatus? status = null)
    {
        return await _db.UserSessions
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x => x.OwnerId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<PagedResult<UserSession>> GetByUserDeviceAsync(
        long deviceId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.UserSessions
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x => x.UserDeviceId == deviceId)
            .ToPagedResultAsync(pagination,
                x => x.Id,
                cancellationToken);
    }

    public async Task<List<UserSession>> GetAllByUserDeviceAsync(
        long deviceId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.UserSessions
           .WithIncludes()
           .WithStatusFilter(status)
           .Where(x => x.UserDeviceId == deviceId)
           .ToListAsync(cancellationToken);
    }

    public async Task<PagedResult<UserSession>> GetByIpAddressAsync(
       string ipAddress,
       PaginationParams pagination,
       CancellationToken cancellationToken = default,
       EntityStatus? status = null)
    {
        return await _db.UserSessions
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x => x.IpAddress == ipAddress)
            .ToPagedResultAsync(pagination,
                x => x.Id,
                cancellationToken);
    }
    public async Task<UserSession?> GetByRefreshTokenHashAsync(
        string refreshTokenHash,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.UserSessions
           .WithIncludes()
           .WithStatusFilter(status)
           .FirstOrDefaultAsync(x => 
                x.RefreshTokenHash == refreshTokenHash);
    }
}
