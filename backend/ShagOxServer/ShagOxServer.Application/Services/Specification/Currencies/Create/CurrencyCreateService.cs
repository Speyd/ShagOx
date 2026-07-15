using ShagOxServer.Application.DTOs.Specification.Currencies.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Create;
using ShagOxServer.Application.Services.Specification.Currencies.Create.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Currencies.Create;
public class CurrencyCreateService : ICurrencyCreateService
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly CurrencyCreateValidator _currencyCreateValidator;

    private readonly IUnitOfWork _unitOfWork;


    public CurrencyCreateService(
        ICurrencyRepository currencyRepository,
        CurrencyCreateValidator currencyCreateValidator,
        IUnitOfWork unitOfWork)
    {
        _currencyRepository = currencyRepository;
        _currencyCreateValidator = currencyCreateValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<CurrencyCreateResponse>> CreateAsync(
        CurrencyCreateRequest request)
    {
        var code = await _currencyCreateValidator
            .ExistsByCodeValidator(request.Code);

        if (!code.IsSuccess)
            return Result<CurrencyCreateResponse>.Fail(code.Error ?? "");


        var name = await _currencyCreateValidator
            .ExistsByNameValidator(request.Name);

        if (!name.IsSuccess)
            return Result<CurrencyCreateResponse>.Fail(code.Error ?? "");

        var currency = CurrencyCreater.CreateCurrency(request);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _currencyRepository.Add(currency);

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