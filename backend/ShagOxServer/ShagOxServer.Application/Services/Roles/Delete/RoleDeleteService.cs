using ShagOxServer.Application.DTOs.Roles.Delete;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Application.Interfaces.Services.Roles.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Roles.Delete;
public class RoleDeleteService : IRoleDeleteService
{
    private readonly IRoleRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public RoleDeleteService(
        IRoleRepository roleRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = roleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RoleDeleteResponse>> DeleteRoleAsync(int id)
    {
        var role = await _repository.GetByIdAsync(id);
        if (role is null)
            return Result<RoleDeleteResponse>.NotFound("Role");

        try
        {
            _repository.Delete(role);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<RoleDeleteResponse>.Success(
           new RoleDeleteResponse(
               role.Id,
               DateTime.UtcNow
           )
       );
    }
}
