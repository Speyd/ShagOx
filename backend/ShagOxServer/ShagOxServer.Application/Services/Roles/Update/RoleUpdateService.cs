using ShagOxServer.Application.DTOs.Roles.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Application.Interfaces.Services.Roles.Update;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Roles.Update;
public class RoleUpdateService : IRoleUpdateService
{
    private readonly IRoleRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public RoleUpdateService(
        IRoleRepository roleRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = roleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RoleUpdateResponse>> UpdateRoleAsync(
        int roleId,
        RoleUpdateRequest request)
    {
        var role = await _repository.GetByIdAsync(roleId);

        if (role is null)
            return Result<RoleUpdateResponse>.NotFound("Role");

        var updatedCount = ApplyUpdates(role, request);
        var result = new RoleUpdateResponse(
                DateTime.UtcNow,
                updatedCount
            );

        if (updatedCount == 0)
            return Result<RoleUpdateResponse>.Success(result);


        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _repository.Update(role);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<RoleUpdateResponse>.Success(result);
    }

    private static int ApplyUpdates(
        Role role,
        RoleUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.Name is not null)
        {
            role.Name = request.Name;
            countUpdated++;
        }

        if (request.Description is not null)
        {
            role.Description = request.Description;
            countUpdated++;
        }

        return countUpdated;
    }
}