using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Auth;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Auth;
using ShagOxServer.Application.Interfaces.Services.Auth.Externals;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts.UserNames;
using ShagOxServer.Application.Interfaces.Services.Jwt;
using ShagOxServer.Application.Services.Auth.Users.Core.Create;
using ShagOxServer.Application.Services.Auth.Users.Roles;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Auth.Externals;
public partial class GoogleLoginService 
    : IGoogleLoginService
{
    private readonly GoogleSettings _googleSettings;

    private readonly IRegisterService _register;

    private readonly IRepository<User> _userRepository;
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IUserNameService _userNameService;
    private readonly UserCreater _userCreater;

    private readonly UserRoleService _roleService;

    private readonly IJwtService _jwtService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GoogleLoginService> _logger;


    public GoogleLoginService(
        IOptions<GoogleSettings> googleSettings,
        IRegisterService register,
        IRepository<User> userRepository,
        IUserQueryRepository userQueryRepository,
        IUserNameService userNameService,
        UserCreater userCreater,
        UserRoleService roleService,
        IJwtService jwtService,
        IUnitOfWork unitOfWork,
        ILogger<GoogleLoginService> logger)
    {
        _googleSettings = googleSettings.Value;
        _register = register;
        _userRepository = userRepository;
        _userQueryRepository = userQueryRepository;
        _userNameService = userNameService;
        _userCreater = userCreater;
        _roleService = roleService;
        _jwtService = jwtService;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
}
