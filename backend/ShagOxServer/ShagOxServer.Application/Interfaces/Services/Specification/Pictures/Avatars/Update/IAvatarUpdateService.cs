using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Request;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Update;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Update;
public interface IAvatarUpdateService
    : IPictureUpdateService<Avatar, PictureUpdateRequest>
{
}
