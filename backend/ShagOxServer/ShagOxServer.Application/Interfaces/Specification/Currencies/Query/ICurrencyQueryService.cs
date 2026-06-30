using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Specification.Currencies;


namespace ShagOxServer.Application.Interfaces.Specification.Currencies.Query;
public interface ICurrencyQueryService
{
    Task<Result<CurrencyDto>> GetByIdAsync(int id);

    Task<Result<CurrencyDto>> GetByCodeAsync(string code);

    Task<Result<CurrencyDto>> GetBySymbolAsync(string symbol);
}
