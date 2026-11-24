using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserDevices;
public class UserDeviceCommandsRepository
    : CommandRepository<UserDevice>,
    IUserDeviceCommandsRepository
{
    public UserDeviceCommandsRepository(
        AppDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }


    public async Task<bool> MarkAsTrasteAsync(
        long deviceId,
        CancellationToken cancellationToken = default)
    {
        var affected = await _db.UserDevices
            .Where(x => x.Id == deviceId)
            .ExecuteUpdateAsync(
                setters => setters.SetProperty(
                    x => x.IsTrusted,
                    true),
                cancellationToken);

        return affected > 0;
    }

    public async Task<bool> UnmarkAsTrasteAsync(
        long deviceId,
        CancellationToken cancellationToken = default)
    {
        var affected = await _db.UserDevices
           .Where(x => x.Id == deviceId)
           .ExecuteUpdateAsync(
               setters => setters.SetProperty(
                   x => x.IsTrusted,
                   false),
               cancellationToken);

        return affected > 0;
    }
}
