using ShagOxServer.SharedKernel.Results;
using ShagOxServer.Application.DTOs.Specification.Currencies.Update;

namespace ShagOxServer.Application.Interfaces.Specification.Currencies.Update;
public interface ICurrencyUpdateService
{
    Task<Result<CurrencyUpdateResponse>> UpdateCurrencyAsync(
        int currencyId,
        CurrencyUpdateRequest request);
}
