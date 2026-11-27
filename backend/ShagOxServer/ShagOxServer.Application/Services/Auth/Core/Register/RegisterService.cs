using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Auth.Register;
using ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Providers.Devices;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Auth;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices.Create;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Interfaces.Services.Baskets.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Resources.Auth.Registrations;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Auth.Users.Contacts;
using ShagOxServer.Application.Services.Auth.Users.Contacts.Passwords;
using ShagOxServer.Application.Services.Auth.Users.Core.Create;
using ShagOxServer.Application.Services.Auth.Users.Roles;
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

    private readonly UserCreater _userCreater;
    private readonly UserRoleService _roleService;
    private readonly UserPasswordService _passwordService;
    private readonly UserContactApplier _contactApplier;

    private readonly IDeviceProvider _deviceProvider;
    private readonly IUserDeviceCreateService _userDeviceService;
    private readonly IUserDeviceQueryService _userQueryDeviceService;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RegisterService> _logger;


    public RegisterService(
        IRepository<User> userRepository,
        IUserQueryRepository userQueryRepository,
        IBasketCreateService basketService,
        UserCreater userCreater,
        UserRoleService roleService,
        UserPasswordService passwordService,
        UserContactApplier contactApplier,
        IVerificationSender senderVerification,
        IDeviceProvider deviceProvider,
        IUserDeviceCreateService userDeviceService,
        IUserDeviceQueryService userQueryDeviceService,
        IUnitOfWork unitOfWork,
        ILogger<RegisterService> logger)
    {
        _userRepository = userRepository;
        _userQueryRepository = userQueryRepository;
        _basketService = basketService;
        _userCreater = userCreater;
        _roleService = roleService;
        _passwordService = passwordService;
        _contactApplier = contactApplier;
        _senderVerification = senderVerification;
        _deviceProvider = deviceProvider;
        _userDeviceService = userDeviceService;
        _userQueryDeviceService = userQueryDeviceService;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<RegisterResponse>> RegisterAsync(
        RegisterRequest request)
    {
        try
        {
            var device = await _deviceProvider
                .GetDeviceAsync();

            if (device is null)
            {
                return Result<RegisterResponse>.Fail(
                    EntityNamesResources.Device);
            }


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
                var userDevice = await EnsureUserDeviceAsync(
                    user.Id, device.Id);

                if (!userDevice.IsSuccess)
                {
                    return Result<RegisterResponse>.Fail(
                        userDevice.Error);   
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
