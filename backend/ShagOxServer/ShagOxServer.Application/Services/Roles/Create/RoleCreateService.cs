using ShagOxServer.Application.DTOs.Roles.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Application.Interfaces.Services.Roles.Create;
using ShagOxServer.Application.Services.Roles.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Roles.Create;
public class RoleCreateService : IRoleCreateService
{
    private readonly IRoleRepository _repository;
    private readonly RoleValidator _validator;

    private readonly IUnitOfWork _unitOfWork;


    public RoleCreateService(
        IRoleRepository roleRepository,
        RoleValidator validator,
        IUnitOfWork unitOfWork)
    {
        _repository = roleRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RoleCreateResponse>> CreateRoleAsync(
        RoleCreateRequest request)
    {
        var validation = await _validator.NotExistsByNameAsync(request.Name);
        if (!validation.IsSuccess)
            return Result<RoleCreateResponse>.Fail(validation.Error ?? "");

        var role = RoleCreater.CreateRole(request);

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
}