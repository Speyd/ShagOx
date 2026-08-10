using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Delete;
public interface IUserDeleteService
    : IDeleteService<DeleteResponse>
{
}