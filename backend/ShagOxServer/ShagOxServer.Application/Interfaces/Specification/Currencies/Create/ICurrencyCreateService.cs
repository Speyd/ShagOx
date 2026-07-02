using ShagOxServer.SharedKernel.Results;
using ShagOxServer.Application.DTOs.Specification.Currencies.Create;

namespace ShagOxServer.Application.Interfaces.Specification.Currencies.Create;
public interface ICurrencyCreateService
{
    Task<Result<CurrencyCreateResponse>> CreateCurrencyAsync(
       CurrencyCreateRequest request);
}
