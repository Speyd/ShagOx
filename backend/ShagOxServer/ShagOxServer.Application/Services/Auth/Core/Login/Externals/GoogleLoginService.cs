using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Auth;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
using ShagOxServer.Application.Interfaces.Services.Auth;
using ShagOxServer.Application.Interfaces.Services.Auth.Externals;
using ShagOxServer.Application.Interfaces.Services.Auth.Login;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts.UserNames;
using ShagOxServer.Application.Services.Auth.Users.Core.Create;

namespace ShagOxServer.Application.Services.Auth.Login.Externals;

public partial class GoogleLoginService 
    : IGoogleLoginService
{
    private readonly GoogleOptions _googleOptions;

    private readonly IRegisterService _register;
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IUserNameService _userNameService;
    private readonly UserCreater _userCreater;

    private readonly ILoginPolicy _loginPolicy;
    private readonly ILoginSessionService _sessionService;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpClientFactory _httpClientFactory;

    private readonly ILogger<GoogleLoginService> _logger;

    public GoogleLoginService(
        IOptions<GoogleOptions> googleOptions,
        IRegisterService register,
        IUserQueryRepository userQueryRepository,
        IUserNameService userNameService,
        UserCreater userCreater,
        ILoginPolicy loginPolicy,
        ILoginSessionService sessionService,
        IUnitOfWork unitOfWork,
        IHttpClientFactory httpClientFactory,
        ILogger<GoogleLoginService> logger)
    {
        _googleOptions = googleOptions.Value;

        _register = register;
        _userQueryRepository = userQueryRepository;
        _userNameService = userNameService;
        _userCreater = userCreater;

        _loginPolicy = loginPolicy;
        _sessionService = sessionService;

        _unitOfWork = unitOfWork;
        _httpClientFactory = httpClientFactory;

        _logger = logger;
    }
}
