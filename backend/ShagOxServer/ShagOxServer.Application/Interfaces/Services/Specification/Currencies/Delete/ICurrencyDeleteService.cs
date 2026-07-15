using ShagOxServer.Application.DTOs.Specification.Currencies.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Delete;
public interface ICurrencyDeleteService
{
    Task<Result<CurrencyDeleteResponse>> DeleteAsync(
        int id);
}