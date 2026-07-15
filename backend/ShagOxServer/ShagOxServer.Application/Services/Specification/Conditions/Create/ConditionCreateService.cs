using ShagOxServer.Application.DTOs.Specification.Conditions.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Create;
using ShagOxServer.Application.Services.Specification.Conditions.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Conditions.Create;
public class ConditionCreateService : IConditionCreateService
{
    private readonly IConditionRepository _conditionRepository;
    private readonly ConditionValidator _conditionValidator;


    private readonly IUnitOfWork _unitOfWork;


    public ConditionCreateService(
        IConditionRepository conditionRepository,
        ConditionValidator conditionValidator,
        IUnitOfWork unitOfWork)
    {
        _conditionRepository = conditionRepository;
        _conditionValidator = conditionValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<ConditionCreateResponse>> CreateAsync(
        ConditionCreateRequest request)
    {
        var validationName = await _conditionValidator.NotExistsByNameAsync(request.Name);
        if(!validationName.IsSuccess)
            Result<ConditionCreateResponse>.Fail(validationName.Error ?? "");

        var condition = ConditionCreater.CreateCondition(request);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _conditionRepository.Add(condition);

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