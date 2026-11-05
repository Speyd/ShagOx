using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Base.Delete;
public interface IDeleteService<TResponse>
{
    Task<Result<TResponse>> DeleteAsync(
        long id,
        CancellationToken cancellationToken = default);
}
