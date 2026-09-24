using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Auth.Roles.Update;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Auth.Roles.Update;
using ShagOxServer.Application.Resources.EntityErrorResourcess;
using ShagOxServer.Application.Services.Auth.Roles.Validator;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Roles.Update;
public class RoleUpdateService 
    : IRoleUpdateService
{
    private readonly IRepository<Role> _roleRepository;
    private readonly RoleValidator _roleValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RoleUpdateService> _logger;


    public RoleUpdateService(
        IRepository<Role> roleRepository,
        RoleValidator roleValidator,
        IUnitOfWork unitOfWork,
        ILogger<RoleUpdateService> logger)
    {
        _roleRepository = roleRepository;
        _roleValidator = roleValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int roleId,
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