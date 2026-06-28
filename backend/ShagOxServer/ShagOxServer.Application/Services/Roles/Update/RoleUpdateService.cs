using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Roles.Update;
using ShagOxServer.Application.Interfaces.Roles.Update;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Interfaces.Auth;

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
        RoleUpdateRequest request)
    {
        var role = await _repository.GetByIdAsync(request.Id);

        if (role is null)
            return Result<RoleUpdateResponse>.Fail(
                "Role not found");

        var updatedCount = ApplyUpdates(role, request);
        if (updatedCount == 0)
            return Result<RoleUpdateResponse>.Fail(
                "No fields to update");

        await _repository.UpdateAsync(role);

        return Result<RoleUpdateResponse>.Success(
            new RoleUpdateResponse(
                DateTime.UtcNow,
                updatedCount
            )
        );
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
