using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Delete;

public interface IAvatarDeleteService
    : IDeleteService<DeleteResponse>
{
    Task<Result<DeleteResponse>> DeleteInternalAsync(
        Avatar avatar);
}