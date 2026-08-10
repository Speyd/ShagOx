using ShagOxServer.Application.DTOs.Auth.Roles.Update;
using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Auth.Roles.Update;
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


    public RoleUpdateService(
        IRepository<Role> roleRepository,
        RoleValidator roleValidator,
        IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _roleValidator = roleValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int roleId,
        RoleUpdateRequest request)
    {
        var role = await _roleValidator.GetByIdAsync(roleId);
        if (!role.IsSuccess)
            return Result<UpdateResponse>.Fail(role.Error);

        if (role.Value!.Name is not null)
        {
            var nameValidator = await _roleValidator.
                NotExistsByNameAsync(role.Value!.Name);

            if (!nameValidator.IsSuccess)
                return Result<UpdateResponse>.Fail(nameValidator.Error);
        }

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
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<UpdateResponse>.Success(result);
    }
}