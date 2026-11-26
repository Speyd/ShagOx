using ShagOxServer.Application.Interfaces.Providers.Devices;
using ShagOxServer.Application.Interfaces.Providers.User;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserSessions;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserSessions.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Management;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;

namespace ShagOxServer.Application.Services.Auth.Users.Management.Trast;
public partial class UserTrastManagementService
    : IUserTrastManagementService
{
    private readonly IUserContext _userContext;
    private readonly IUserQueryService _userService;

    private readonly IDeviceProvider _deviceProvider;
    private readonly IUserDeviceCommandsRepository _deviceCommandRepository;
    private readonly IUserDeviceQueryService _deviceQueryService;

    private readonly ISessionProvider _sessionProvider;
    private readonly IUserSessionCommandsRepository _sessionCommandRepository;
    private readonly IUserSessionQueryRepository _sessionQueryRepository;


    public UserTrastManagementService(
        IUserContext userContext,
        IUserQueryService userService,
        IDeviceProvider deviceProvider,
        IUserDeviceCommandsRepository deviceCommandRepository,
        IUserDeviceQueryService deviceQueryService,
        ISessionProvider sessionProvider,
        IUserSessionCommandsRepository sessionCommandRepository,
        IUserSessionQueryRepository sessionQueryRepository)
    {
        _userContext = userContext;
        _userService = userService;
        _deviceProvider = deviceProvider;
        _deviceCommandRepository = deviceCommandRepository;
        _deviceQueryService = deviceQueryService;
        _sessionProvider = sessionProvider;
        _sessionCommandRepository = sessionCommandRepository;
        _sessionQueryRepository = sessionQueryRepository;
    }
}
