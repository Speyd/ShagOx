using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserDevices;
public class UserDeviceExistsRepository
    : ExistsOwnerRepository<UserDevice>,
      IUserDeviceExistsRepository
{
    public UserDeviceExistsRepository(
        AppDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }

    public async Task<bool> IsBlockedAsync(
        long userId,
        long deviceId,
        CancellationToken cancellationToken)
    {
        return await _db.UserDevices
            .AsNoTracking()
            .AnyAsync(
                d => d.OwnerId == userId &&
                     d.DeviceId == deviceId &&
                     d.Status == UserDeviceStatus.Blocked,
                cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        long userId,
        long userDeviceId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.UserDevices
           .WithStatusFilter(status)
           .AnyAsync(x =>
                x.OwnerId == userId &&
                x.Id == userDeviceId,
                cancellationToken
       );
    }
}
