using Microsoft.AspNet.Identity;
using ShagOxServer.Application.Common.Validators;
using ShagOxServer.Application.DTOs.Auth.Register;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Auth;
using ShagOxServer.Application.Interfaces.Services.Common.Validators;
using ShagOxServer.Application.Interfaces.Services.Verifications;
using ShagOxServer.Application.Services.Auth.Users.Create;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Account.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShagOxServer.Application.Services.Auth;
public class RegisterService 
    : IRegisterService
{
    private readonly IRepository<User> _userRepository;
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IEmailService _emailService;
    private readonly IVerificationCodeService _codeService;

    private readonly UserCreater _userCreater;
    
    private readonly IUnitOfWork _unitOfWork;


    public RegisterService(
        IRepository<User> userRepository,
        IUserQueryRepository userQueryRepository,
        UserCreater userCreater,
        IEmailService emailService,
        IVerificationCodeService codeService,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _userQueryRepository = userQueryRepository;
        _userCreater = userCreater;
        _emailService = emailService;
        _codeService = codeService;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<RegisterResponse>> RegisterAsync(
        RegisterRequest request)
    {
        try
        {
            var user = _userCreater.CreateUser(request);

            var userGet = await _userQueryRepository
                .GetByContactAsync(user.Email, user.Phone, user.UserName);

            if (userGet is not null)
            {
                if (userGet.Status != UserStatus.PendingVerification)
                {
                    return Result<RegisterResponse>
                        .AlreadyExists(typeof(User));
                }
                else
                {
                    user = userGet;
                }
            }

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                await _userCreater.AddDefaultRole(user);

                if(userGet is null)
                    _userRepository.Add(user);

                await _userCreater.SetDefaultName(user);
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

            await _unitOfWork.CommitAsync();

            await SendCode(user);

            return Result<RegisterResponse>.Success(
               new RegisterResponse(user)
            );
        }
        catch(Exception ex)
        {
            return Result<RegisterResponse>.Fail(ex.Message);
        }
    }

    private async Task SendCode(
        User user)
    {
        if (!string.IsNullOrWhiteSpace(user.Email))
        {
            var code = await _codeService
                .CreateCodeAsync(user.Id);

            await _unitOfWork.SaveChangesAsync();

            await _emailService
                .SendVerificationCodeAsync(user.Email, code);
        }
        else if (!string.IsNullOrWhiteSpace(user.Phone))
        {
        }
    }
}