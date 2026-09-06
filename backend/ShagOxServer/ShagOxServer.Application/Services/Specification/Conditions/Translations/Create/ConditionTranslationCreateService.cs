using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Specification.Conditions.Translations.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Create;
using ShagOxServer.Application.Services.Specification.Conditions.Translations.Validator;
using ShagOxServer.Domain.Entities.Specification.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Conditions.Translations.Create;
public class ConditionTranslationCreateService
    : IConditionTranslationCreateService
{
    private readonly IRepository<ConditionTranslation> _conditionRepository;
    private readonly ConditionTranslationValidator _conditionValidator;

    private readonly IUnitOfWork _unitOfWork;


    public ConditionTranslationCreateService(
        IRepository<ConditionTranslation> conditionRepository,
        ConditionTranslationValidator conditionValidator,
        IUnitOfWork unitOfWork)
    {
        _conditionRepository = conditionRepository;
        _conditionValidator = conditionValidator;

        _unitOfWork = unitOfWork;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        ConditionTranslationCreateRequest request)
    {
        var codeValidation = await _conditionValidator
            .NotExistsAsync(request.ConditionId, request.Language);

        if (!codeValidation.IsSuccess)
            return Result<CreateResponse>.Fail(codeValidation.Error);


        var city = ConditionTranslationCreater.Create(request);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _conditionRepository.Add(city);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<CreateResponse>.Success(
            new CreateResponse(
                city.Id,
                DateTime.UtcNow
        ));
    }
}