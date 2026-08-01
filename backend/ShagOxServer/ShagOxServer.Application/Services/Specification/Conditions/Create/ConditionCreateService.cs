using ShagOxServer.Application.DTOs.Common.Responses;
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


    public async Task<Result<CreateResponse>> CreateAsync(
        ConditionCreateRequest request)
    {
        var validationName = await _conditionValidator
            .NotExistsByNameAsync(request.Name);

        if(!validationName.IsSuccess)
            Result<CreateResponse>.Fail(validationName.Error);

        var condition = ConditionCreater.Create(request);

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

        return Result<CreateResponse>.Success(
            new CreateResponse(
                condition.Id,
                DateTime.UtcNow
        ));
    }
}