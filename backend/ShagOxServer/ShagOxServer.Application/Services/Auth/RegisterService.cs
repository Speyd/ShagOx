using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.DTOs.Auth.Register;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Auth;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Services.Auth.Users.Contacts;
using ShagOxServer.Application.Services.Auth.Users.Contacts.Passwords;
using ShagOxServer.Application.Services.Auth.Users.Core.Create;
using ShagOxServer.Application.Services.Auth.Users.Roles;
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
    private readonly UserRoleService _roleService;
    private readonly UserPasswordService _passwordService;
    private readonly UserContactApplier _contactApplier;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RegisterService> _logger;


    public RegisterService(
        IRepository<User> userRepository,
        IUserQueryRepository userQueryRepository,
        UserCreater userCreater,
        UserRoleService roleService,
        UserPasswordService passwordService,
        UserContactApplier contactApplier,
        IVerificationSender senderVerification,
        IUnitOfWork unitOfWork,
        ILogger<RegisterService> logger)
    {
        _userRepository = userRepository;
        _userQueryRepository = userQueryRepository;
        _userCreater = userCreater;
        _roleService = roleService;
        _passwordService = passwordService;
        _contactApplier = contactApplier;
        _senderVerification = senderVerification;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<RegisterResponse>> RegisterAsync(
        RegisterRequest request)
    {
        try
        {
            var prepareResult =
                await PrepareUserForRegistrationAsync(
                    request
            );
            
            if (!prepareResult.IsSuccess)

            {
                return Result<RegisterResponse>
                    .Fail(prepareResult.Error);
            }

            var user = prepareResult.Value!.User;

            await _unitOfWork.BeginTransactionAsync();
            
            try
            {
                if (prepareResult is not null &&
                    !prepareResult.Value.IsExisting)
                {
                    _userRepository.Add(user);


                    var roleResult = await _roleService
                        .AddDefaultRoleAsync(user);

                    if (!roleResult.IsSuccess)
                    {
                        return Result<RegisterResponse>
                            .Fail(roleResult.Error);

                    }

                    await _unitOfWork.SaveChangesAsync();
                }

                await _userCreater.SetDefaultName(user);
            }
            catch(Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Registration failed.");

                await _unitOfWork.RollbackAsync();

                return Result<RegisterResponse>
                    .Fail("Registration failed.");
            }

            await _unitOfWork.CommitAsync();
  
            var verificationResult = await SendVerification(user);

            if (!verificationResult.IsSuccess)
                return Result<RegisterResponse>.Fail(verificationResult.Error);

            _logger.LogInformation(
                "User registered successfully. UserId: {UserId}",
                user.Id);

            return Result<RegisterResponse>.Success(
               new RegisterResponse(user)
            );
        }
        catch(Exception ex)
        {
            _logger.LogError(
                ex,
                "Registration failed. Contact: {Contact}",
                request.EmailOrPhone);

            return Result<RegisterResponse>
                .Fail("Registration failed.");
        }
    }

    private async Task<Result<(User User, bool IsExisting)>> 
        PrepareUserForRegistrationAsync(
            RegisterRequest request)
    {
        bool isExisting = false;
        User? user = null;

        var userGet = await _userQueryRepository
            .GetByContactAsync(request.EmailOrPhone);

        if (userGet is not null)
        {         
            if (userGet.Status != UserStatus.PendingVerification)
            {
                return Result<(User, bool)>
                    .AlreadyExists(typeof(User));
            }

            user = userGet;

            var result = await _contactApplier
                .ApplyAsync(user, request);

            if (!result.IsSuccess)

            {
                return Result<(User User, bool IsExisting)>
                    .Fail(result.Error);
            }

            isExisting = true;
        }
        else
        {
            var userResult = await _userCreater
                .CreateUser(request);

            if (!userResult.IsSuccess)

            {
                return Result<(User User, bool IsExisting)>
                    .Fail(userResult.Error);
            }

            user = userResult.Value;
        }

        var passResult = _passwordService
            .Apply(user!, request);

        if (!passResult.IsSuccess)

        {
            return Result<(User User, bool IsExisting)>
                .Fail(passResult.Error);
        }

        return Result<(User, bool)>
            .Success((user!, isExisting));
    }

    private async Task<Result<bool>> SendVerification(
        User user)
    {
        user.Status = UserStatus.PendingVerification;

        if (!string.IsNullOrWhiteSpace(user.Email))
        {
            return await _senderVerification
                .SendAsync(user,
                    VerificationCodePurpose.RegistrationEmail);
        }

        if (!string.IsNullOrWhiteSpace(user.Phone))
        {
            return await _senderVerification
                .SendAsync(user, 
                    VerificationCodePurpose.RegistrationPhone);
        }

        return Result<bool>.Fail("Email or phone is required for verification.");
    }
}