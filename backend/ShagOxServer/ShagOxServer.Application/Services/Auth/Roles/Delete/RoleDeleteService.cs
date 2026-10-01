using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Auth.Roles.Delete;
using ShagOxServer.Application.Interfaces.Services.Auth.UserRoles.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Auth.Roles.Validator;
using ShagOxServer.Application.Services.Caches.Auth;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Roles.Delete;
public class RoleDeleteService 
    : IRoleDeleteService
{
    private readonly IRepository<Role> _roleRepository;
    private readonly RoleValidator _roleValidator;

    private readonly IUserRoleQueryService _userRoleService;
    private readonly IAdvertisementQueryRepository _advertRepository;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RoleDeleteService> _logger;
    private readonly ICacheService _cache;


    public RoleDeleteService(
        IRepository<Role> roleRepository,
        RoleValidator roleValidator,
        IUserRoleQueryService userRoleService,
        IAdvertisementQueryRepository advertRepository,
        IUnitOfWork unitOfWork,
        ILogger<RoleDeleteService> logger,
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


    public async Task<Result<DeleteResponse>> DeleteAsync(
        long id)
    {
        var role = await _roleValidator
            .GetByIdAsync(id);

        if (!role.IsSuccess)
            return Result<DeleteResponse>.Fail(role.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _roleRepository.Delete(role.Value!);

            await _unitOfWork.CommitAsync();

            await RoleCache.InvalidateDeleteAsync(
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
                "Failed to delete role. Id: {Id}",
                id);

            return Result<DeleteResponse>
                .Fail(EntityErrorResources.RoleDeleteFailed);
        }

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               role.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}