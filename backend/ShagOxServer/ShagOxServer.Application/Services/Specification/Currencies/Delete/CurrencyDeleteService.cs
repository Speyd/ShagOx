using ShagOxServer.Application.DTOs.Specification.Currencies.Delete;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Delete;
using ShagOxServer.Application.Services.Specification.Conditions.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Currencies.Delete;
public class CurrencyDeleteService : ICurrencyDeleteService
{
    private readonly IConditionRepository _currencyRepository;
    private readonly ConditionValidator _currencyValidator;

    private readonly IUnitOfWork _unitOfWork;


    public CurrencyDeleteService(
        IConditionRepository currencyRepository,
        ConditionValidator currencyValidator,
        IUnitOfWork unitOfWork)
    {
        _currencyRepository = currencyRepository;
        _currencyValidator = currencyValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<CurrencyDeleteResponse>> DeleteAsync(
        int id)
    {
        var currency = await _currencyValidator.GetByIdAsync(id);
        if (!currency.IsSuccess)
            return Result<CurrencyDeleteResponse>.Fail(currency.Error ?? "");


        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _currencyRepository.Delete(currency.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<CurrencyDeleteResponse>.Success(
          new CurrencyDeleteResponse(
              currency.Value!.Id,
              DateTime.UtcNow
          )
      );
    }
}