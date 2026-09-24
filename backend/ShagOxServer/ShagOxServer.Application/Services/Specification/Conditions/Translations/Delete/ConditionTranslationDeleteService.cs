using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Specification.Conditions.Translations.Validator;
using ShagOxServer.Domain.Entities.Specification.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Conditions.Translations.Delete;
public class ConditionTranslationDeleteService
    : IConditionTranslationDeleteService
{
    private readonly IRepository<ConditionTranslation> _conditionRepository;
    private readonly ConditionTranslationValidator _conditionValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ConditionTranslationDeleteService> _logger;


    public ConditionTranslationDeleteService(
        IRepository<ConditionTranslation> conditionRepository,
        ConditionTranslationValidator conditionValidator,
        IUnitOfWork unitOfWork,
        ILogger<ConditionTranslationDeleteService> logger)
    {
        _conditionRepository = conditionRepository;
        _conditionValidator = conditionValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        int id)
    {
        var city = await _conditionValidator
            .GetByIdAsync(id);

        if (!city.IsSuccess)
            return Result<DeleteResponse>.Fail(city.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _conditionRepository.Delete(city.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to delete condition translation. Id: {Id}",
                id);

            return Result<DeleteResponse>
                 .Fail(EntityError.ConditionTranslationDeleteFailed);
        }

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               city.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}