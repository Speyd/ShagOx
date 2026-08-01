using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Delete;
public interface ICurrencyDeleteService
{
    Task<Result<DeleteResponse>> DeleteAsync(
        int id);
}