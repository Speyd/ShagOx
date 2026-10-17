using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Base;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Update;
public interface IPictureUpdateService<TEntity, TRequest>
    : IUpdateService<UpdateResponse,
        TRequest>
    where TEntity : BaseEntity
    where TRequest: PictureUpdateRequest
{
    Task<Result<UpdateResponse>> UpdateInternalAsync(
        TEntity entity,
        TRequest request);
}
