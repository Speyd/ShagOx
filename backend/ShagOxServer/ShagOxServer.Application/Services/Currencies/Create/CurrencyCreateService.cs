using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Specification.Currencies.Create;
using ShagOxServer.Application.Interfaces.Specification.Currencies.Create;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Infrastructure.Interfaces.Specification;

namespace ShagOxServer.Application.Services.Currencies.Create;
public class CurrencyCreateService : ICurrencyCreateService
{
    private readonly ICurrencyRepository _repository;

    public CurrencyCreateService(
        ICurrencyRepository currencyRepository)
    {
        _repository = currencyRepository;
    }

    public async Task<Result<CurrencyCreateResponse>> CreateCurrencyAsync(
        CurrencyCreateRequest request)
    {
        var currency = CreateCurrency(request);

        await _repository.AddAsync(currency);

        var response = new CurrencyCreateResponse(
            currency.Id,
            DateTime.UtcNow
        );

        return Result<CurrencyCreateResponse>.Success(response);
    }

    private Currency CreateCurrency(
        CurrencyCreateRequest request)
    {
        return new Currency
        {
            Code = request.Code,
            Symbol = request.Symbol,
            Name = request.Name,
        };
    }
}
