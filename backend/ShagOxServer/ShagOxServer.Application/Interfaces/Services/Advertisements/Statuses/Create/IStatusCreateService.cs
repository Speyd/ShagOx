using ShagOxServer.Application.DTOs.Advertisements.Statuses.Create;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Create;
public interface IStatusCreateService
    : ICreateService<
        CreateResponse,
        StatusCreateRequest
        >
{
}