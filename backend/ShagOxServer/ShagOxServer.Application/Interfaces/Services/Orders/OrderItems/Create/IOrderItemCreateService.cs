using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.LineItems.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Base.Create;

namespace ShagOxServer.Application.Interfaces.Services.Orders.OrderItems.Create;
public interface IOrderItemCreateService
    : ICreateService<
        CreateResponse,
        LineItemCreateRequest
        >
{
}
