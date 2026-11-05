using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Base.Delete;
public interface ISoftDeleteService<TResponse>
{
    Task<Result<TResponse>> SoftDeleteAsync(
       long id);
}
