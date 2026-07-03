using ShagOxServer.Application.DTOs.Roles.Update;
using ShagOxServer.Application.Interfaces.Roles.Update;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Interfaces.Auth.Roles;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Roles.Update;

public class RoleUpdateService : IRoleUpdateService
{
    private readonly IRoleRepository _repository;

    public RoleUpdateService(
        IRoleRepository roleRepository)
    {
        _repository = roleRepository;
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

        await _repository.UpdateAsync(role);

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
