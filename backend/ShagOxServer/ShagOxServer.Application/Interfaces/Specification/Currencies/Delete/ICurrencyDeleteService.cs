using ShagOxServer.Application.DTOs.Specification.Currencies.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Specification.Currencies.Delete;
public interface ICurrencyDeleteService
{
    Task<Result<CurrencyDeleteResponse>> DeleteCurrencyAsync(
        int id);
}
