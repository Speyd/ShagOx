using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Base;
public interface IUpdateService<TResponse, TRequest>
{
    Task<Result<TResponse>> UpdateAsync(
        long id,
        TRequest request);
}