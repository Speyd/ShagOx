using ShagOxServer.Application.DTOs.Auth.Register;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Auth;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Services.Auth.Users.Core.Create;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Account.Enum;
using ShagOxServer.Domain.Entities.Verifications.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth;
public class RegisterService 
    : IRegisterService
{
    private readonly IRepository<User> _userRepository;
    private readonly IUserQueryRepository _userQueryRepository;

    private readonly IVerificationSender _senderVerification;

    private readonly UserCreater _userCreater;
    
    private readonly IUnitOfWork _unitOfWork;


    public RegisterService(
        IRepository<User> userRepository,
        IUserQueryRepository userQueryRepository,
        UserCreater userCreater,
        IVerificationSender senderVerification,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _userQueryRepository = userQueryRepository;
        _userCreater = userCreater;
        _senderVerification = senderVerification;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<RegisterResponse>> RegisterAsync(
        RegisterRequest request)
    {
        try
        {
            var user = await _userCreater.CreateUser(request);
            if (!user.IsSuccess)
                Result<RegisterResponse>.Fail(user.Error);


            var result = await PrepareUserForRegistrationAsync(
                user.Value!, request);

            if (!result.IsSuccess)
                Result<RegisterResponse>.Fail(result.Error);

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                if (result is not null &&
                    !result.Value.IsExisting)
                {
                    _userRepository.Add(result?.Value.User!);

                    await _userCreater.AddDefaultRole(result?.Value.User!);

                    await _userCreater.SetDefaultName(result?.Value.User!);
                }
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

            await _unitOfWork.CommitAsync();
  
            await SendVerification(result?.Value.User!);

            return Result<RegisterResponse>.Success(
               new RegisterResponse(result?.Value.User!)
            );
        }
        catch(Exception ex)
        {
            return Result<RegisterResponse>.Fail(ex.Message);
        }
    }

    private async Task<Result<(User User, bool IsExisting)>> 
        PrepareUserForRegistrationAsync(
            User user,
            RegisterRequest request)
    {
        bool isExisting = false;

        var userGet = await _userQueryRepository
            .GetByContactAsync(user.Email, user.Phone);

        if (userGet is not null)
        {
            if (userGet.Status != UserStatus.PendingVerification)
            {
                return Result<(User, bool)>.AlreadyExists(typeof(User));
            }

            user = userGet;
            var result = await _userCreater.ApplyContact(user, request);
            if (!result.IsSuccess)
                Result<(User User, bool IsExisting)>.Fail(result.Error);

            isExisting = true;
        }

        _userCreater.CreatePasswordHash(user, request);

        return Result<(User, bool)>.Success((user, isExisting));
    }

    private async Task SendVerification(
        User user)
    {
        user.Status = UserStatus.PendingVerification;

        if (!string.IsNullOrWhiteSpace(user.Email))
        {
            await _senderVerification
                .SendAsync(user,
                    VerificationCodePurpose.RegistrationEmail);
        }
        else if (!string.IsNullOrWhiteSpace(user.Phone))
        {
            await _senderVerification
                .SendAsync(user, 
                    VerificationCodePurpose.RegistrationPhone);
        }
    }
}