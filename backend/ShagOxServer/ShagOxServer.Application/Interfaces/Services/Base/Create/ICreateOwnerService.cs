using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Base.Create;
public interface ICreateOwnerService<TResponse, TRequest>
{
    Task<Result<TResponse>> CreateAsync(
        TRequest request,
        long ownerId);
}
