using ShagOxServer.Application.DTOs.Specification.Currencies.Create;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Specification.Currencies.Create;
public interface ICurrencyCreateService
{
    Task<Result<CurrencyCreateResponse>> CreateCurrencyAsync(
       CurrencyCreateRequest request);
}
