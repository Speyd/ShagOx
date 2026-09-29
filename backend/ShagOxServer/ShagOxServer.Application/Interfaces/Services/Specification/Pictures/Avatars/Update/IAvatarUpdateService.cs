using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Update;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Update;
public interface IAvatarUpdateService
    : IUpdateService<UpdateResponse, AvatarUpdateRequest>
{
    Task<Result<UpdateResponse>> UpdateInternalAsync(
        Avatar avatar,
        AvatarUpdateRequest request);
}