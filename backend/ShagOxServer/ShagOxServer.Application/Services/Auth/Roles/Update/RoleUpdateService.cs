using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Auth.Roles.Update;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Auth.Roles.Update;
using ShagOxServer.Application.Interfaces.Services.Auth.UserRoles.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Auth.Roles.Validator;
using ShagOxServer.Application.Services.Auth.Users.Roles;
using ShagOxServer.Application.Services.Caches.Auth;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Roles.Update;
public class RoleUpdateService 
    : IRoleUpdateService
{
    private readonly IRepository<Role> _roleRepository;
    private readonly RoleValidator _roleValidator;

    private readonly IUserRoleQueryService _userRoleService;
    private readonly IAdvertisementQueryRepository _advertRepository;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RoleUpdateService> _logger;
    private readonly ICacheService _cache;


    public RoleUpdateService(
        IRepository<Role> roleRepository,
        RoleValidator roleValidator,
        IUserRoleQueryService userRoleService,
        IAdvertisementQueryRepository advertRepository,
        IUnitOfWork unitOfWork,
        ILogger<RoleUpdateService> logger,
        ICacheService cache)
    {
        _roleRepository = roleRepository;
        _roleValidator = roleValidator;
        _userRoleService = userRoleService;
        _advertRepository = advertRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _cache = cache;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        long roleId,
        RoleUpdateRequest request)
    {
        var role = await _roleValidator.GetByIdAsync(roleId);
        if (!role.IsSuccess)
            return Result<UpdateResponse>.Fail(role.Error);

        var validation = await
             ValidateUpdatesAsync(role.Value!, request);

        if (!validation.IsSuccess)
            return Result<UpdateResponse>.Fail(validation.Error);


        var updatedCount = RoleUpdater.ApplyUpdates(role.Value!, request);
        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(result);


        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _roleRepository.Update(role.Value!);

            await _unitOfWork.CommitAsync();

            await RoleCache.InvalidateUpdateAsync(
                _cache, _userRoleService, role.Value!);

            await UserCache.InvalidateByRoleAsync(
                _cache,
                _userRoleService,
                _advertRepository,
                role.Value!.Id
                );
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to update role. Id: {Id}",
                roleId);

            return Result<UpdateResponse>
                .Fail(EntityErrorResources.RoleUpdateFailed);
        }

        return Result<UpdateResponse>.Success(result);
    }

    private async Task<Result<bool>> ValidateUpdatesAsync(
        Role role,
        RoleUpdateRequest request)
    {
        if (request.Name is not null &&
           request.Name != role.Name)
        {
            var nameValidator = await _roleValidator.
                NotExistsByNameAsync(request.Name);

            if (!nameValidator.IsSuccess)
                return Result<bool>.Fail(nameValidator.Error);
        }

        return Result<bool>.Success(true);
    }
}