using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Core;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Management.Moderation;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Management.Trast;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices.Query;

namespace ShagOxServer.Application.Services.Auth.Users.Management.Moderation;
public partial class UserModerationService
    : IUserModerationService
{
    private readonly IUserQueryService _userService;
    private readonly IUserCommandsRepository _userCommandsRepository;

    private readonly IUserDeviceQueryService _deviceService;
    private readonly IUserDeviceCommandsRepository _deviceCommandsRepository;

    private readonly IUserTrastManagementService _trastManagement;


    public UserModerationService(
        IUserQueryService userService,
        IUserCommandsRepository userCommandsRepository,
        IUserDeviceQueryService deviceService,
        IUserDeviceCommandsRepository deviceCommandsRepository,
        IUserTrastManagementService trastManagement)
    {
        _userService = userService;
        _userCommandsRepository = userCommandsRepository;
        _deviceService = deviceService;
        _deviceCommandsRepository = deviceCommandsRepository;
        _trastManagement = trastManagement;
    }
}
