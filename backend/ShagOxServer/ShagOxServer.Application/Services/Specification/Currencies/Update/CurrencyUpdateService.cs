using ShagOxServer.Application.DTOs.Specification.Currencies.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Update;
using ShagOxServer.Application.Services.Specification.Currencies.Validator;
using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Currencies.Update;
public class CurrencyUpdateService : ICurrencyUpdateService
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly CurrencyValidator _currencyValidator;

    private readonly IUnitOfWork _unitOfWork;


    public CurrencyUpdateService(
        ICurrencyRepository currencyRepository,
        CurrencyValidator currencyValidator,
        IUnitOfWork unitOfWork)
    {
        _currencyRepository = currencyRepository;
        _currencyValidator = currencyValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int currencyId,
        CurrencyUpdateRequest request)
    {
        if (request.Code is not null)
        {
            var codeValidator = await _currencyValidator
                .ExistsByCodeValidator(request.Code);

            if (!codeValidator.IsSuccess)
                return Result<UpdateResponse>.Fail(codeValidator.Error);
        }

        if (request.Name is not null)
        {
            var nameValidator = await _currencyValidator
            .ExistsByNameValidator(request.Name);

            if (!nameValidator.IsSuccess)
                return Result<UpdateResponse>.Fail(nameValidator.Error);
        }


        var currency = await _currencyValidator
            .GetByIdAsync(currencyId);

        if (!currency.IsSuccess)
            return Result<UpdateResponse>.Fail(currency.Error);


        var updatedCount = CurrencyUpdater
            .ApplyUpdates(currency.Value!, request);


        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(result);


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

        return Result<UpdateResponse>.Success(result);
    }
}