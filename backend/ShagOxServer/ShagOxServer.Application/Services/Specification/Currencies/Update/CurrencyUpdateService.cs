using ShagOxServer.Application.DTOs.Specification.Currencies.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Currencies.Update;
using ShagOxServer.Application.Services.Specification.Currencies.Update.Validator;
using ShagOxServer.Application.Services.Specification.Currencies.Validator;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Currencies.Update;
public class CurrencyUpdateService : ICurrencyUpdateService
{
    private readonly ICurrencyRepository _repository;
    private readonly CurrencyValidator _validator;
    private readonly CurrencyUpdateValidator _updateValidator;

    private readonly IUnitOfWork _unitOfWork;


    public CurrencyUpdateService(
        ICurrencyRepository currencyRepository,
        CurrencyValidator validator,
        CurrencyUpdateValidator updateValidator,
        IUnitOfWork unitOfWork)
    {
        _repository = currencyRepository;
        _validator = validator;
        _updateValidator = updateValidator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CurrencyUpdateResponse>> UpdateCurrencyAsync(
        int currencyId,
        CurrencyUpdateRequest request)
    {
        var code = await _updateValidator.ExistsByCodeValidator(request.Code);
        if (!code.IsSuccess)
            return Result<CurrencyUpdateResponse>.Fail(code.Error ?? "");

        var name = await _updateValidator.ExistsByNameValidator(request.Name);
        if (!name.IsSuccess)
            return Result<CurrencyUpdateResponse>.Fail(code.Error ?? "");

        var currency = await _validator.GetByIdAsync(currencyId);
        if (!currency.IsSuccess)
            return Result<CurrencyUpdateResponse>.Fail(currency.Error ?? "");

        var updatedCount = ApplyUpdates(currency.Value!, request);
        var result = new CurrencyUpdateResponse(
                DateTime.UtcNow,
                updatedCount
            );

        if (updatedCount == 0)
            return Result<CurrencyUpdateResponse>.Success(result);


        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _repository.Update(currency.Value!);

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