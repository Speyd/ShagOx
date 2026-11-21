using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.LineItems.LineItemAttributes.Create;
using ShagOxServer.Application.Interfaces.Services.Base.Create;

namespace ShagOxServer.Application.Interfaces.Services.LineItems.BasketAttributes.Create;
public interface ILineItemAttributeCreateService
    : ICreateService<
        CreateResponse,
        LineItemAttributeCreateRequest
        >
{
}
