using ShagOxServer.Application.DTOs.Auth.Roles.Create;
using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Auth.Roles.Create;
using ShagOxServer.Application.Services.Auth.Roles.Validator;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Roles.Create;
public class RoleCreateService : IRoleCreateService
{
    private readonly IRepository<Role> _roleRepository;
    private readonly RoleValidator _roleValidator;

    private readonly IUnitOfWork _unitOfWork;


    public RoleCreateService(
        IRepository<Role> roleRepository,
        RoleValidator roleValidator,
        IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _roleValidator = roleValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        RoleCreateRequest request)
    {
        var validation = await _roleValidator
            .NotExistsByNameAsync(request.Name);

        if (!validation.IsSuccess)
            return Result<CreateResponse>.Fail(validation.Error);

        var role = RoleCreater.Create(request);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _roleRepository.Add(role);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<CreateResponse>.Success(
            new CreateResponse(
                role.Id,
                DateTime.UtcNow
        ));
    }
}