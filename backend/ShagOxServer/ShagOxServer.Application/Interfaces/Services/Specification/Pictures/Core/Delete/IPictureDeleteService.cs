using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Delete;
public interface IPictureDeleteService<TEntity>
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<DeleteResponse>
    where TEntity : BasePicture
{
    Task<Result<DeleteResponse>> DeleteInternalAsync(
        TEntity entity);

    Task<Result<DeleteResponse>> DeleteRecordAsync(
        long id);
}
