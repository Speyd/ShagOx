using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Auth.UserRoles.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Delete;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.Application.Services.Auth.Users.Roles;
using ShagOxServer.Application.Services.Caches.Advertisements;
using ShagOxServer.Application.Services.Caches.Auth;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Core.Delete;
public class UserDeleteService 
    : IUserDeleteService
{
    private readonly IRepository<User> _userRepository;
    private readonly UserValidator _userValidator;

    private readonly IAvatarDeleteService _avatarDeleteService;
    private readonly IUserRoleQueryService _userRoleService;
    private readonly IAdvertisementQueryRepository _advertRepository;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserDeleteService> _logger;
    private readonly ICacheService _cache;



    public UserDeleteService(
        IRepository<User> userRepository,
        UserValidator userValidator,
        IAvatarDeleteService avatarDeleteService,
        IUserRoleQueryService userRoleService,
        IAdvertisementQueryRepository advertRepository,
        IUnitOfWork unitOfWork,
        ILogger<UserDeleteService> logger,
        ICacheService cache)
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
        _avatarDeleteService = avatarDeleteService;
        _userRoleService = userRoleService;
        _advertRepository = advertRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _cache = cache;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        long id)
    {
        var user = await _userValidator.GetByIdAsync(id);
        if (!user.IsSuccess)
            return Result<DeleteResponse>.Fail(user.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _userRepository.Delete(user.Value!);

            if (user.Value!.Avatar is not null)
            {
                await _avatarDeleteService
                    .DeleteInternalAsync(user.Value!.Avatar);
            }

            await _unitOfWork.CommitAsync();

            await CacheInvalidate(user.Value);
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to delete user. Id: {Id}",
                id);

            return Result<DeleteResponse>
                .Fail(EntityErrorResources.UserDeleteFailed);
        }

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               user.Value!.Id,
               DateTime.UtcNow
           )
       );
    }

    private async Task CacheInvalidate(
        User user)
    {
        await UserCache.InvalidateDeleteAsync(
                _cache, _userRoleService, user);

        await AdvertisementCache.InvalidateByUserAsync(
            _cache, _advertRepository, user.Id);
    }
}