using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Request;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Response;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Base;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Update;
public interface IPictureUpdateService<TEntity, TRequest>
    : IUpdateService<PictureUpdateResponse,
        TRequest>
    where TEntity : BaseEntity
    where TRequest: PictureUpdateRequest
{
    Task<Result<PictureUpdateResponse>> UpdateInternalAsync(
        TEntity entity,
        TRequest request);

    Task RollbackAsync(
        TEntity entity,
        PictureUpdateResponse response);
}
