using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Specification.Conditions.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Create;
using ShagOxServer.Application.Services.Specification.Conditions.Validator;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Conditions.Create;
public class ConditionCreateService
    : IConditionCreateService
{
    private readonly IRepository<Condition> _conditionRepository;
    private readonly ConditionValidator _conditionValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ConditionCreateService> _logger;


    public ConditionCreateService(
        IRepository<Condition> conditionRepository,
        ConditionValidator conditionValidator,
        IUnitOfWork unitOfWork,
        ILogger<ConditionCreateService> logger)
    {
        _conditionRepository = conditionRepository;
        _conditionValidator = conditionValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        ConditionCreateRequest request)
    {
        var validationName = await _conditionValidator
            .NotExistsByCodeAsync(request.Code);

        if(!validationName.IsSuccess)
            Result<CreateResponse>.Fail(validationName.Error);

        var condition = ConditionCreater.Create(request);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _conditionRepository.Add(condition);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to create condition. Code: {Code}",
                request.Code);

            return Result<CreateResponse>
                 .Fail("Failed to create condition.");
        }

        return Result<CreateResponse>.Success(
            new CreateResponse(
                condition.Id,
                DateTime.UtcNow
        ));
    }
}