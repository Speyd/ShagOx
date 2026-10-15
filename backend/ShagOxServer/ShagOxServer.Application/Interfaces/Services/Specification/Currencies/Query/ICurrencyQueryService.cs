using ShagOxServer.Application.DTOs.Specification.Currencies.Query;
using ShagOxServer.Application.Interfaces.Services.Base.Query;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Filters.Specification.Currencies;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Query;
public interface ICurrencyQueryService
    : IQueryService<CurrencyDto, Currency, CurrencySearchFilter>
{
    Task<Result<CurrencyDto>> GetByCodeAsync(
        string code,
        CancellationToken cancellationToken = default);

    Task<Result<CurrencyDto>> GetBySymbolAsync(
        string symbol,
        CancellationToken cancellationToken = default);
}
