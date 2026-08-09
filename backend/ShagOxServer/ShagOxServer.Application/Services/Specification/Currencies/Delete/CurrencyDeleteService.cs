using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Delete;
using ShagOxServer.Application.Services.Specification.Currencies.Validator;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Currencies.Delete;
public class CurrencyDeleteService : ICurrencyDeleteService
{
    private readonly IRepository<Currency> _currencyRepository;
    private readonly CurrencyValidator _currencyValidator;

    private readonly IUnitOfWork _unitOfWork;


    public CurrencyDeleteService(
        IRepository<Currency> currencyRepository,
        CurrencyValidator currencyValidator,
        IUnitOfWork unitOfWork)
    {
        _currencyRepository = currencyRepository;
        _currencyValidator = currencyValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        int id)
    {
        var currency = await _currencyValidator.GetByIdAsync(id);
        if (!currency.IsSuccess)
            return Result<DeleteResponse>.Fail(currency.Error);


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

        return Result<DeleteResponse>.Success(
          new DeleteResponse(
              currency.Value!.Id,
              DateTime.UtcNow
          )
      );
    }
}