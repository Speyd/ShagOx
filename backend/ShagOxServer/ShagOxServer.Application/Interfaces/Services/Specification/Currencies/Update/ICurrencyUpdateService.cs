using ShagOxServer.Application.DTOs.Specification.Currencies.Update;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.Application.DTOs.Common.Responses;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Update;
public interface ICurrencyUpdateService
{
    Task<Result<UpdateResponse>> UpdateAsync(
        int currencyId,
        CurrencyUpdateRequest request);
}