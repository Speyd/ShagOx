using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Specification.Currencies;

namespace ShagOxServer.Application.Interfaces.Specification.Currencies.Query;
public interface ICurrencyQueryService
{
    Task<Result<CurrencyDto>> GetByIdAsync(int id);

    Task<Result<CurrencyDto>> GetByCodeAsync(string code);

    Task<Result<CurrencyDto>> GetBySymbolAsync(string symbol);

    Task<Result<List<CurrencyDto>>> SearchByCode(
      string code,
      int page,
      int pageSize);

    Task<Result<List<CurrencyDto>>> SearchByName(
      string name,
      int page,
      int pageSize);
}
