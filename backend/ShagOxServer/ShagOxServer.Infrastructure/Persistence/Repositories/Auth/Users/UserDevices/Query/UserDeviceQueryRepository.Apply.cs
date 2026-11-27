using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Filters.Auth.Users.UserDevices;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserDevices.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserDevices.Query;
public partial class UserDeviceQueryRepository
    : SearchRepository<UserDevice, UserDeviceSearchFilter>,
      IUserDeviceQueryRepository
{
    protected override IQueryable<UserDevice> ApplyIncludes(
        IQueryable<UserDevice> query,
        EntityStatus? status = null)
    {
        return query.WithIncludes(status);
    }

    public override IQueryable<UserDevice> ApplyFilter(
        IQueryable<UserDevice> query,
        UserDeviceSearchFilter filter)
    {
        return query.Filter(filter);
    }
}
