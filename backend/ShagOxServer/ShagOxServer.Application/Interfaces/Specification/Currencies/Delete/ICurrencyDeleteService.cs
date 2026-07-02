using ShagOxServer.SharedKernel.Results;
using ShagOxServer.Application.DTOs.Specification.Currencies.Delete;

namespace ShagOxServer.Application.Interfaces.Specification.Currencies.Delete;
public interface ICurrencyDeleteService
{
    Task<Result<CurrencyDeleteResponse>> DeleteCurrencyAsync(
        int id);
}
