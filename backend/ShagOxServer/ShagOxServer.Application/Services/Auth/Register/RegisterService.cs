using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Auth.Register;
using ShagOxServer.Application.DTOs.Baskets.Core.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Auth;
using ShagOxServer.Application.Interfaces.Services.Baskets.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Resources.Auth.Registrations;
using ShagOxServer.Application.Services.Auth.Users.Contacts;
using ShagOxServer.Application.Services.Auth.Users.Contacts.Passwords;
using ShagOxServer.Application.Services.Auth.Users.Core.Create;
using ShagOxServer.Application.Services.Auth.Users.Roles;
using ShagOxServer.Domain.Entities.Account;
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
                if (!prepareResult.Value.IsExisting)
                {
                    var createResult = await CreateUserAggregateAsync(user);

                    if (!createResult.IsSuccess)
                    {
                        return Result<RegisterResponse>
                            .Fail(createResult.Error);
                    }
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

    private async Task<Result<bool>> CreateUserAggregateAsync(
        User user)
    {
        _userRepository.Add(user);

        var basketResult = await _basketService.CreateAsync(
            new BasketCreateRequest(user.Id));

        if (!basketResult.IsSuccess)
        {
            return Result<bool>
                .Fail(basketResult.Error);
        }

        var roleResult = await _roleService
            .AddDefaultRoleAsync(user);

        if (!roleResult.IsSuccess)
        {
            return Result<bool>
                .Fail(roleResult.Error);
        }

        await _unitOfWork.SaveChangesAsync();

        return Result<bool>.Success(true);
    }
}