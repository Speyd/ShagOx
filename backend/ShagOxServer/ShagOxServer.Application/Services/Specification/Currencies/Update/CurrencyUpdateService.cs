using ShagOxServer.Application.DTOs.Specification.Currencies.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Currencies.Update;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Currencies.Update;
public class CurrencyUpdateService : ICurrencyUpdateService
{
    private readonly ICurrencyRepository _repository;
    private readonly ICurrencyExistsRepository _existsRepository;
    private readonly IUnitOfWork _unitOfWork;


    public CurrencyUpdateService(
        ICurrencyRepository currencyRepository,
        ICurrencyExistsRepository existsRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = currencyRepository;
        _existsRepository = existsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CurrencyUpdateResponse>> UpdateCurrencyAsync(
        int currencyId,
        CurrencyUpdateRequest request)
    {
        if (request.Code is not null &&
            await _existsRepository.ExistsByCodeAsync(request.Code))
        {
            return Result<CurrencyUpdateResponse>
                .AlreadyExists("Currency code");
        }
        if (request.Name is not null &&
            await _existsRepository.ExistsByNameAsync(request.Name))
        {
            return Result<CurrencyUpdateResponse>
                .AlreadyExists("Currency name");
        }

        var currency = await _repository.GetByIdAsync(currencyId);
        if (currency is null)
            return Result<CurrencyUpdateResponse>.NotFound("Currency");

        var updatedCount = ApplyUpdates(currency, request);
        var result = new CurrencyUpdateResponse(
                DateTime.UtcNow,
                updatedCount
            );

        if (updatedCount == 0)
            return Result<CurrencyUpdateResponse>.Success(result);


        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _repository.Update(currency);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<CurrencyUpdateResponse>.Success(result);
    }

    private static int ApplyUpdates(
        Currency currency,
        CurrencyUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.Code is not null)
        {
            currency.Code = request.Code;
            countUpdated++;
        }

        if (request.Symbol is not null)
        {
            currency.Symbol = request.Symbol;
            countUpdated++;
        }

        if (request.Name is not null)
        {
            currency.Name = request.Name;
            countUpdated++;
        }

        return countUpdated;
    }
}
