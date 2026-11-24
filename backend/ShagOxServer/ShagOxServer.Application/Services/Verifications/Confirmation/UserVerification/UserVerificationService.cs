using ShagOxServer.Application.Interfaces.Services.Auth.Users.Management;
using ShagOxServer.Application.Interfaces.Services.Verifications.Confirmation.UserVerification;

namespace ShagOxServer.Application.Services.Verifications.Confirmation.UserVerification;
public partial class UserVerificationService
    : IUserVerificationService
{
    private readonly IUserTrastManagementService _trastManagement;


    public UserVerificationService(
        IUserTrastManagementService trastManagement)
    {
        _trastManagement = trastManagement;
    }
}
