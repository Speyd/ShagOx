using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
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
    private readonly ILogger<ConditionTranslationCreateService> _logger;


    public ConditionTranslationCreateService(
        IRepository<ConditionTranslation> conditionRepository,
        ConditionTranslationValidator conditionValidator,
        IUnitOfWork unitOfWork,
        ILogger<ConditionTranslationCreateService> logger)
    {
        _conditionRepository = conditionRepository;
        _conditionValidator = conditionValidator;

        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        ConditionTranslationCreateRequest request)
    {
        var codeValidation = await _conditionValidator
            .NotExistsAsync(request.TranslatableId, request.Language);

        if (!codeValidation.IsSuccess)
            return Result<CreateResponse>.Fail(codeValidation.Error);


        var city = ConditionTranslationCreater.Create(request);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _conditionRepository.Add(city);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to create condition translation. " +
                "TranslatableId: {TranslatableId}",
                request.TranslatableId);

            return Result<CreateResponse>
                 .Fail("Failed to create condition translation.");
        }

        return Result<CreateResponse>.Success(
            new CreateResponse(
                city.Id,
                DateTime.UtcNow
        ));
    }
}