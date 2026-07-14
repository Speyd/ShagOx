using ShagOxServer.Application.DTOs.Specification.Conditions.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Conditions.Create;
using ShagOxServer.Application.Services.Specification.Conditions.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Conditions.Create;
public class ConditionCreateService : IConditionCreateService
{
    private readonly IConditionRepository _repository;
    private readonly ConditionValidator _validator;


    private readonly IUnitOfWork _unitOfWork;


    public ConditionCreateService(
        IConditionRepository conditionRepository,
        ConditionValidator validator,
        IUnitOfWork unitOfWork)
    {
        _repository = conditionRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ConditionCreateResponse>> CreateConditionAsync(
        ConditionCreateRequest request)
    {
        var validationName = await _validator.NotExistsByNameAsync(request.Name);
        if(!validationName.IsSuccess)
            Result<ConditionCreateResponse>.Fail(validationName.Error ?? "");

        var condition = ConditionCreater.CreateCondition(request);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _repository.Add(condition);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        var response = new ConditionCreateResponse(
            condition.Id,
            DateTime.UtcNow
        );

        return Result<ConditionCreateResponse>.Success(response);
    }
}