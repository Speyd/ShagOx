using ShagOxServer.Application.DTOs.Roles.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Application.Interfaces.Services.Roles.Update;
using ShagOxServer.Application.Services.Roles.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Roles.Update;
public class RoleUpdateService : IRoleUpdateService
{
    private readonly IRoleRepository _repository;
    private readonly RoleValidator _validator;

    private readonly IUnitOfWork _unitOfWork;


    public RoleUpdateService(
        IRoleRepository roleRepository,
        RoleValidator validator,
        IUnitOfWork unitOfWork)
    {
        _repository = roleRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<RoleUpdateResponse>> UpdateRoleAsync(
        int roleId,
        RoleUpdateRequest request)
    {
        var role = await _validator.GetByIdAsync(roleId);
        if (!role.IsSuccess)
            return Result<RoleUpdateResponse>.Fail(role.Error ?? "");

        var updatedCount = RoleUpdater.ApplyUpdates(role.Value!, request);
        var result = new RoleUpdateResponse(
                DateTime.UtcNow,
                updatedCount
            );

        if (updatedCount == 0)
            return Result<RoleUpdateResponse>.Success(result);


        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _repository.Update(role.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<RoleUpdateResponse>.Success(result);
    }
}