using ShagOxServer.Application.DTOs.Roles.Delete;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Application.Interfaces.Services.Roles.Delete;
using ShagOxServer.Application.Services.Roles.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Roles.Delete;
public class RoleDeleteService : IRoleDeleteService
{
    private readonly IRoleRepository _roleRepository;
    private readonly RoleValidator _roleValidator;

    private readonly IUnitOfWork _unitOfWork;


    public RoleDeleteService(
        IRoleRepository roleRepository,
        RoleValidator roleValidator,
        IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _roleValidator = roleValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<RoleDeleteResponse>> DeleteAsync(int id)
    {
        var role = await _roleValidator.GetByIdAsync(id);
        if (!role.IsSuccess)
            return Result<RoleDeleteResponse>.Fail(role.Error ?? "");

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _roleRepository.Delete(role.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<RoleDeleteResponse>.Success(
           new RoleDeleteResponse(
               role.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}