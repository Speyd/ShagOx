using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.LineItems.LineItemAttributes.Update;
using ShagOxServer.Application.Interfaces.Services.Base.Update;

namespace ShagOxServer.Application.Interfaces.Services.LineItems.BasketAttributes.Update;
public interface ILineItemAttributeUpdateService
    : IUpdateService<UpdateResponse, LineItemAttributeUpdateRequest>,
    IEntityStatusUpdateService
{
}
