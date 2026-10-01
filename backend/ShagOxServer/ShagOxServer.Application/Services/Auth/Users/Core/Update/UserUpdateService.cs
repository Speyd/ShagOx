using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Update;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Auth.UserRoles.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Update;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.Application.Services.Caches.Advertisements;
using ShagOxServer.Application.Services.Caches.Auth;
using ShagOxServer.Application.Services.Location.Cities.Validator;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Update;
public class UserUpdateService 
    : IUserUpdateService
{
    private readonly IRepository<User> _userRepository;
    private readonly UserValidator _userValidator;

    private readonly CityValidator _cityValidator;

    private readonly IAvatarDeleteService _avatarDeleteService;
    private readonly IAvatarCreateService _avatarCreateService;
    private readonly IUserRoleQueryService _userRoleService;
    private readonly IAdvertisementQueryRepository _advertRepository;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserUpdateService> _logger;
    private readonly ICacheService _cache;


    public UserUpdateService(
        IRepository<User> userRepository,
        UserValidator userValidator,
        CityValidator cityValidator,
        IAvatarDeleteService avatarDeleteService,
        IAvatarCreateService avatarCreateService,
        IUserRoleQueryService userRoleService,
        IAdvertisementQueryRepository advertRepository,
        IUnitOfWork unitOfWork,
        ILogger<UserUpdateService> logger,
        ICacheService cache)
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
        _cityValidator = cityValidator;
        _avatarDeleteService = avatarDeleteService;
        _avatarCreateService = avatarCreateService;
        _userRoleService = userRoleService;
        _advertRepository = advertRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _cache = cache;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        long userId,
        UserUpdateRequest request)
    {
        var user = await _userValidator
            .GetByIdWithIncludesAsync(userId);
        if (!user.IsSuccess)
            return Result<UpdateResponse>.Fail(user.Error);

        var userName = request.UserName?.Trim();
        var normalizedRequest = request with
        {
            UserName = userName
        };


        var validation = await
            ValidateUpdatesAsync(user.Value!, normalizedRequest);

        if (!validation.IsSuccess)
            return Result<UpdateResponse>.Fail(validation.Error);


        var updatedCount = UserUpdater
            .ApplyUpdates(user.Value!, normalizedRequest);

        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(result);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _userRepository.Update(user.Value!);

            if(normalizedRequest.Avatar is not null)
                await UpdateAvatarAsync(user.Value!,
                    normalizedRequest.Avatar
                );

            await _unitOfWork.CommitAsync();

            await CacheInvalidate(user.Value!);
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to update user. Id: {Id}",
                userId);

            return Result<UpdateResponse>
                .Fail(EntityErrorResources.UserUpdateFailed);
        }

        return Result<UpdateResponse>.Success(result);
    }

    private async Task CacheInvalidate(
        User user)
    {
        await UserCache.InvalidateUpdateAsync(
                _cache, _userRoleService, user);

        await AdvertisementCache.InvalidateByUserAsync(
            _cache, _advertRepository, user.Id);
    }

    private async Task<Result<bool>> ValidateUpdatesAsync(
        User user,
        UserUpdateRequest request)
    {
        if (request.CityId.HasValue &&
           request.CityId != user.CityId)
        {
            var cityExists = await _cityValidator
                .ExistsByIdAsync(request.CityId.Value);

            if (!cityExists.IsSuccess)
                return Result<bool>.Fail(cityExists.Error);
        }

        if (request.UserName is not null &&
            request.UserName != user.UserName)
        {
            var userNameExists = await _userValidator
                .NotExistsByUserNameAsync(request.UserName);

            if (!userNameExists.IsSuccess)
                return Result<bool>.Fail(userNameExists.Error);
        }

        return Result<bool>.Success(true);
    }

    private async Task UpdateAvatarAsync(
        User user,
        IFormFile avatarFile)
    {
        var oldAvatar = user.Avatar;

        var result = await _avatarCreateService.CreateInternalAsync(
            new AvatarCreateRequest(
                avatarFile,
                user.Id));

        if (!result.IsSuccess)
            throw new Exception(result.Error?.ToString());

        if (oldAvatar is not null)
        {
            await _avatarDeleteService.DeleteInternalAsync(oldAvatar);
        }
    }
}