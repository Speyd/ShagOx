using ShagOxServer.Application.DTOs.Auth.Users.Core.Update;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Update;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Update;
public interface IUserUpdateService
    : IUpdateService<UpdateResponse, UserUpdateRequest>,
    IEntityStatusUpdateService
{
}
