using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Orders.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Base.Create;

namespace ShagOxServer.Application.Interfaces.Services.Orders.Core.Create;
public interface IOrderCreateService
    : ICreateOwnerService<
        CreateResponse,
        OrderCreateRequest
        >
{
}
