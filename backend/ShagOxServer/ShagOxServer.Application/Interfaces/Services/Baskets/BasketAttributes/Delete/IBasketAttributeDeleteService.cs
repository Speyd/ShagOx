using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;

namespace ShagOxServer.Application.Interfaces.Services.Baskets.BasketAttributes.Delete;
public interface IBasketAttributeDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<DeleteResponse>
{
}
