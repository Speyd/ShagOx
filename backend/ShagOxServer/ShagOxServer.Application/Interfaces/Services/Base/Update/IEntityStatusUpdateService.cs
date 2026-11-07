using ShagOxServer.Application.DTOs.Base.Requests;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Base.Update;
public interface IEntityStatusUpdateService
{
    public Task<Result<bool>> ChangeStatusAsync(
       long entityId,
       ChangeStatusRequest request,
       CancellationToken cancellationToken = default);
}
