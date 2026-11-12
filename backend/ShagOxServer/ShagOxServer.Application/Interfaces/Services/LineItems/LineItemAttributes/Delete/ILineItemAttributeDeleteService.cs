using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.LineItems;

namespace ShagOxServer.Application.Interfaces.Services.LineItems.BasketAttributes.Delete;
public interface ILineItemAttributeDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<LineItemAttribute,DeleteResponse>
{
}
