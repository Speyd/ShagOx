using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices;
using ShagOxServer.Application.Interfaces.Services.Auth.Core.Login;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Management.Trast;
using ShagOxServer.Application.Interfaces.Services.Verifications.Confirmation.UserVerification;

namespace ShagOxServer.Application.Services.Verifications.Confirmation.UserVerification;
public partial class UserVerificationService
    : IUserVerificationService
{
    private readonly IUserTrastManagementService _trastManagement;
    private readonly IUserDeviceCommandsRepository _deviceCommandsRepository;
    private readonly ILoginSessionService _sessionService;


    public UserVerificationService(
        IUserTrastManagementService trastManagement,
        IUserDeviceCommandsRepository deviceCommandsRepository,
        ILoginSessionService sessionService)
    {
        _trastManagement = trastManagement;
        _deviceCommandsRepository = deviceCommandsRepository;
        _sessionService = sessionService;
    }
}
