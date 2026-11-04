using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Domain.Base;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Base.Delete;
public interface ISoftDeleteService<TEntity, TResponse>
    where TEntity : BaseEntity
{
    Task<Result<TResponse>> SoftDeleteAsync(
       long id);

    Task<Result<TResponse>> SoftDeleteRecordAsync(
       TEntity entity);

    Task<Result<TResponse>> SoftDeleteInternalAsync(
        TEntity entity);
}
