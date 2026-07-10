using ShagOxServer.Application.DTOs.Roles.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Application.Interfaces.Services.Roles.Create;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Roles.Create;
public class RoleCreateService : IRoleCreateService
{
    private readonly IRoleRepository _repository;
    private readonly IRoleExistsRepository _existsRepository;
    private readonly IUnitOfWork _unitOfWork;


    public RoleCreateService(
        IRoleRepository roleRepository,
        IRoleExistsRepository existsRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = roleRepository;
        _existsRepository = existsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RoleCreateResponse>> CreateRoleAsync(
        RoleCreateRequest request)
    {
        var validation = await _existsRepository.ExistsAsync(request.Name);
        if (validation)
            return Result<RoleCreateResponse>.AlreadyExists("Role");

        var role = CreateRole(request);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _repository.Add(role);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

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
