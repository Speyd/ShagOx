using ShagOxServer.Application.DTOs.Auth.Users.Core.Update;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Update;
public interface IUserUpdateService
    : IUpdateService<UpdateResponse, UserUpdateRequest>
{
}