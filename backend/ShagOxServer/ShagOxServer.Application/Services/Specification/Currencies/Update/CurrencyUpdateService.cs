using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Specification.Currencies.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Update;
using ShagOxServer.Application.Services.Specification.Currencies.Validator;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Currencies.Update;
public class CurrencyUpdateService 
    : ICurrencyUpdateService
{
    private readonly IRepository<Currency> _currencyRepository;
    private readonly CurrencyValidator _currencyValidator;

    private readonly IUnitOfWork _unitOfWork;


    public CurrencyUpdateService(
        IRepository<Currency> currencyRepository,
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
        var currency = await _currencyValidator
            .GetByIdAsync(currencyId);

        if (!currency.IsSuccess)
            return Result<UpdateResponse>.Fail(currency.Error);


        var validation = await 
            ValidateUpdatesAsync(currency.Value!, request);

        if (!validation.IsSuccess)
            return Result<UpdateResponse>.Fail(validation.Error);


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

    private async Task<Result<bool>> ValidateUpdatesAsync(
        Currency currency,
        CurrencyUpdateRequest request)
    {
        if (request.Code is not null &&
            request.Code != currency.Code)
        {
            var result = await _currencyValidator
                .NotExistsByCodeValidator(request.Code);

            if (!result.IsSuccess)
                return Result<bool>.Fail(result.Error!);
        }

        if (request.Name is not null &&
            request.Name != currency.Name)
        {
            var result = await _currencyValidator
                .NotExistsByNameValidator(request.Name);

            if (!result.IsSuccess)
                return Result<bool>.Fail(result.Error!);
        }

        return Result<bool>.Success(true);
    }
}