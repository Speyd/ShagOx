using ShagOxServer.Application.DTOs.Specification.Currencies.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Update;
using ShagOxServer.Application.Services.Specification.Currencies.Update.Validator;
using ShagOxServer.Application.Services.Specification.Currencies.Validator;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Currencies.Update;
public class CurrencyUpdateService : ICurrencyUpdateService
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly CurrencyValidator _currencyValidator;
    private readonly CurrencyUpdateValidator _currencyUpdateValidator;

    private readonly IUnitOfWork _unitOfWork;


    public CurrencyUpdateService(
        ICurrencyRepository currencyRepository,
        CurrencyValidator currencyValidator,
        CurrencyUpdateValidator currencyUpdateValidator,
        IUnitOfWork unitOfWork)
    {
        _currencyRepository = currencyRepository;
        _currencyValidator = currencyValidator;
        _currencyUpdateValidator = currencyUpdateValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<CurrencyUpdateResponse>> UpdateAsync(
        int currencyId,
        CurrencyUpdateRequest request)
    {
        var code = await _currencyUpdateValidator
            .ExistsByCodeValidator(request.Code);
        if (!code.IsSuccess)
            return Result<CurrencyUpdateResponse>.Fail(code.Error ?? "");


        var name = await _currencyUpdateValidator
            .ExistsByNameValidator(request.Name);

        if (!name.IsSuccess)
            return Result<CurrencyUpdateResponse>.Fail(code.Error ?? "");


        var currency = await _currencyValidator
            .GetByIdAsync(currencyId);

        if (!currency.IsSuccess)
            return Result<CurrencyUpdateResponse>.Fail(currency.Error ?? "");

        var updatedCount = CurrencyUpdater.ApplyUpdates(currency.Value!, request);
        var result = new CurrencyUpdateResponse(
                DateTime.UtcNow,
                updatedCount
            );

        if (updatedCount == 0)
            return Result<CurrencyUpdateResponse>.Success(result);


        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _currencyRepository.Update(currency.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<CurrencyUpdateResponse>.Success(result);
    }
}