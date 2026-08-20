using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Create;
using ShagOxServer.Application.DTOs.Specification.Pictures.Create;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Create;
public interface IAvatarCreateService
    : ICreateService<
        PictureCreateResponse,
        AvatarCreateRequest
        >
{
}