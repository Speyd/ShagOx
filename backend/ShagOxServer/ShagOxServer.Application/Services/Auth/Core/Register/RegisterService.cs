using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Auth.Register;
using ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Create;
using ShagOxServer.Application.DTOs.Baskets.Core.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Auth;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices.Create;
using ShagOxServer.Application.Interfaces.Services.Baskets.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Resources.Auth.Registrations;
using ShagOxServer.Application.Services.Auth.Users.Contacts;
using ShagOxServer.Application.Services.Auth.Users.Contacts.Passwords;
using ShagOxServer.Application.Services.Auth.Users.Core.Create;
using ShagOxServer.Application.Services.Auth.Users.Core.Mapping;
using ShagOxServer.Application.Services.Auth.Users.Roles;
using ShagOxServer.Application.Services.Caches.Invalidations.Auth;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Register;
public partial class RegisterService 
    : IRegisterService
{
    private readonly IRepository<User> _userRepository;
    private readonly IUserQueryRepository _userQueryRepository;

    private readonly IBasketCreateService _basketService;

    private readonly IVerificationSender _senderVerification;

    private readonly UserInvalidationService _userInvalid;

    private readonly UserCreater _userCreater;
    private readonly UserRoleService _roleService;
    private readonly UserPasswordService _passwordService;
    private readonly UserContactApplier _contactApplier;

    private readonly IUserDeviceCreateService _userDeviceService;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RegisterService> _logger;


    public RegisterService(
        IRepository<User> userRepository,
        IUserQueryRepository userQueryRepository,
        IBasketCreateService basketService,
        UserCreater userCreater,
        UserInvalidationService userInvalid,
        UserRoleService roleService,
        UserPasswordService passwordService,
        UserContactApplier contactApplier,
        IVerificationSender senderVerification,
        IUserDeviceCreateService userDeviceService,
        IUnitOfWork unitOfWork,
        ILogger<RegisterService> logger)
    {
        _userRepository = userRepository;
        _userQueryRepository = userQueryRepository;
        _basketService = basketService;
        _userCreater = userCreater;
        _userInvalid = userInvalid;
        _roleService = roleService;
        _passwordService = passwordService;
        _contactApplier = contactApplier;
        _senderVerification = senderVerification;
        _userDeviceService = userDeviceService;
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
                    request);

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
                    var createResult = await CreateUserAggregateAsync(user);

                    if (!createResult.IsSuccess)
                    {
                        await _unitOfWork.RollbackAsync();

                        return Result<RegisterResponse>
                            .Fail(createResult.Error);
                    }
                }

                var deviceResult = await _userDeviceService.CreateInternalAsync(
                    new UserDeviceCreateRequest(
                        user.Id, 
                        request.DeviceIdentifier
                ));

                if (!deviceResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();

                    return Result<RegisterResponse>
                        .Fail(deviceResult.Error);
                }

                await _userCreater.SetDefaultName(user);
            }
            catch(Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Registration failed.");

                await _unitOfWork.RollbackAsync();

                return Result<RegisterResponse>.Fail(
                    RegistrationAuthResources.RegistrationFailed);
            }

            await _unitOfWork.CommitAsync();


            var verificationResult = await SendVerification(user);

            if (!verificationResult.IsSuccess)
                return Result<RegisterResponse>.Fail(verificationResult.Error);


            _logger.LogInformation(
                "User registered successfully. UserId: {UserId}",
                user.Id);

            await _userInvalid.InvalidateCreateAsync(
                UserCacheMapper.ToInfo(user));

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

            return Result<RegisterResponse>.Fail(
                RegistrationAuthResources.RegistrationFailed);
        }
    }
}
