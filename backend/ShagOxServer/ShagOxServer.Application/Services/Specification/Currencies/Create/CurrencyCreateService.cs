using ShagOxServer.Application.DTOs.Specification.Currencies.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Currencies.Create;
using ShagOxServer.Application.Services.Specification.Currencies.Create.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Currencies.Create;
public class CurrencyCreateService : ICurrencyCreateService
{
    private readonly ICurrencyRepository _repository;
    private readonly CurrencyCreateValidator _validator;

    private readonly IUnitOfWork _unitOfWork;


    public CurrencyCreateService(
        ICurrencyRepository currencyRepository,
        CurrencyCreateValidator validator,
        IUnitOfWork unitOfWork)
    {
        _repository = currencyRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CurrencyCreateResponse>> CreateCurrencyAsync(
        CurrencyCreateRequest request)
    {
        var code = await _validator.ExistsByCodeValidator(request.Code);
        if (!code.IsSuccess)
            return Result<CurrencyCreateResponse>.Fail(code.Error ?? "");

        var name = await _validator.ExistsByNameValidator(request.Name);
        if (!name.IsSuccess)
            return Result<CurrencyCreateResponse>.Fail(code.Error ?? "");

        var currency = CurrencyCreater.CreateCurrency(request);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _repository.Add(currency);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        var response = new CurrencyCreateResponse(
            currency.Id,
            DateTime.UtcNow
        );

        return Result<CurrencyCreateResponse>.Success(response);
    }
}