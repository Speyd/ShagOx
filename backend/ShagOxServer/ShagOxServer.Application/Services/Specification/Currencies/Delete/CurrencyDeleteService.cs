using ShagOxServer.Application.DTOs.Specification.Currencies.Delete;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Currencies.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Currencies.Delete;
public class CurrencyDeleteService : ICurrencyDeleteService
{
    private readonly IConditionRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CurrencyDeleteService(
        IConditionRepository currencyRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = currencyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CurrencyDeleteResponse>> DeleteCurrencyAsync(
        int id)
    {
        var currency = await _repository.GetByIdAsync(id);
        if (currency is null)
            return Result<CurrencyDeleteResponse>.NotFound("Currency");


        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _repository.Delete(currency);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<CurrencyDeleteResponse>.Success(
          new CurrencyDeleteResponse(
              currency.Id,
              DateTime.UtcNow
          )
      );
    }
}
