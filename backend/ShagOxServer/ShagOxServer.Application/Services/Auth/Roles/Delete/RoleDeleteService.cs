using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Auth.Roles.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Auth.Roles.Mapping;
using ShagOxServer.Application.Services.Auth.Roles.Validator;
using ShagOxServer.Application.Services.Caches.Invalidations.Auth;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Roles.Delete;
public class RoleDeleteService 
    : IRoleDeleteService
{
    private readonly IRepository<Role> _roleRepository;
    private readonly RoleValidator _roleValidator;

    private readonly RoleInvalidationService _roleInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RoleDeleteService> _logger;


    public RoleDeleteService(
        IRepository<Role> roleRepository,
        RoleValidator roleValidator,
        RoleInvalidationService roleInvalid,
        IUnitOfWork unitOfWork,
        ILogger<RoleDeleteService> logger)
    {
        _roleRepository = roleRepository;
        _roleValidator = roleValidator;
        _roleInvalid = roleInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
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

        await _roleInvalid.InvalidateDeleteAsync(
            RoleCacheMapper.ToInfo(role.Value!));

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               role.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}