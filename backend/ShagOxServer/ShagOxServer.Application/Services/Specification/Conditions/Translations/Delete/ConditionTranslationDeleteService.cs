using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Delete;
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


    public ConditionTranslationDeleteService(
        IRepository<ConditionTranslation> conditionRepository,
        ConditionTranslationValidator conditionValidator,
        IUnitOfWork unitOfWork)
    {
        _conditionRepository = conditionRepository;
        _conditionValidator = conditionValidator;
        _unitOfWork = unitOfWork;
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
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               city.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}