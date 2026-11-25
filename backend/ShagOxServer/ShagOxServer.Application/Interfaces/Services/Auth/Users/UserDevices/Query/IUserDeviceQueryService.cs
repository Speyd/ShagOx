using ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Interfaces.Services.Base.Query;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Filters.Auth.Users.UserDevices;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices.Query;
public interface IUserDeviceQueryService
    : IQueryService<UserDeviceDto, 
        UserDevice, 
        UserDeviceSearchFilter>
{
}
