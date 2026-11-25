using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Filters.Auth.Users.UserDevices;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Core.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserDevices.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserDevices.Query;
public partial class UserDeviceQueryRepository
    : SearchRepository<UserDevice, UserDeviceSearchFilter>,
      IUserDeviceQueryRepository
{
    public UserDeviceQueryRepository(
        ReplicaDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }


    public async Task<UserDevice?> GetByIdentifierAsync(
        string deviceIdentifier, 
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.UserDevices
            .WithIncludes()
            .WithStatusFilter(status)
            .FirstOrDefaultAsync(x => 
                x.DeviceIdentifier == deviceIdentifier,
                cancellationToken);
    }

    public async Task<PagedResult<UserDevice>> GetByIpAsync(
        string ipAddress,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.UserDevices
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x => x.IpAddress == ipAddress)
            .ToPagedResultAsync(pagination,
                x => x.Id,
                cancellationToken);
    }

    public async Task<PagedResult<UserDevice>> GetByUserAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.UserDevices
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x => x.OwnerId == userId)
            .ToPagedResultAsync(pagination,
                x => x.Id,
                cancellationToken);
    }

    public async Task<List<UserDevice>> GetAllByUserAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.UserDevices
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x => x.OwnerId == userId)
            .ToListAsync(cancellationToken);
    }
}
