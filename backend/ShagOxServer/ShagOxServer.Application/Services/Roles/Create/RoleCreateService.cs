using ShagOxServer.Application.DTOs.Roles.Create;
using ShagOxServer.Application.Interfaces.Roles.Create;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Interfaces.Auth.Roles;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Roles.Create;
public class RoleCreateService : IRoleCreateService
{
    private readonly IRoleRepository _repository;
    private readonly IRoleExistsRepository _existsRepository;


    public RoleCreateService(
        IRoleRepository roleRepository,
        IRoleExistsRepository existsRepository)
    {
        _repository = roleRepository;
        _existsRepository = existsRepository;
    }

    public async Task<Result<RoleCreateResponse>> CreateRoleAsync(
        RoleCreateRequest request)
    {
        var validation = await _existsRepository.ExistsAsync(request.Name);
        if (validation)
            return Result<RoleCreateResponse>.AlreadyExists("Role");

        var role = CreateRole(request);

        await _repository.AddAsync(role);

        var response = new RoleCreateResponse(
            role.Id,
            DateTime.UtcNow
        );

        return Result<RoleCreateResponse>.Success(response);
    }

    private Role CreateRole(
       RoleCreateRequest request)
    {
        return new Role
        {
            Name = request.Name,
            Description = request.Description ?? "",
        };
    }
}
