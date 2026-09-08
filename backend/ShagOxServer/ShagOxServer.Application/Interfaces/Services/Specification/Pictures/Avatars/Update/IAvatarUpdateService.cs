using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Update;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Update;
public interface IAvatarUpdateService
    : IUpdateService<UpdateResponse, AvatarUpdateRequest>
{
}