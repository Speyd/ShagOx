using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Domain.Base;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Base.Delete;
public interface ISoftDeleteService<TEntity, TResponse>
    where TEntity : BaseEntity
{
    Task<Result<TResponse>> SoftDeleteAsync(
       long id,
        CancellationToken cancellationToken = default);

    Task<Result<TResponse>> SoftDeleteRecordAsync(
       TEntity entity,
        CancellationToken cancellationToken = default);

    Task<Result<TResponse>> SoftDeleteInternalAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);
}
