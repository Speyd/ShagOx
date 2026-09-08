using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Base;
public interface IDeleteService<TResponse>
{
    Task<Result<TResponse>> DeleteAsync(
        int id);
}