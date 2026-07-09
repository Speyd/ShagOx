using ShagOxServer.Application.DTOs.Specification.Currencies.Update;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Roles.Specification.Currencies.Update;
public interface ICurrencyUpdateService
{
    Task<Result<CurrencyUpdateResponse>> UpdateCurrencyAsync(
        int currencyId,
        CurrencyUpdateRequest request);
}
