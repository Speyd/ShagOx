using ShagOxServer.Application.DTOs.Specification.Currencies.Update;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Update;
public interface ICurrencyUpdateService
{
    Task<Result<CurrencyUpdateResponse>> UpdateAsync(
        int currencyId,
        CurrencyUpdateRequest request);
}