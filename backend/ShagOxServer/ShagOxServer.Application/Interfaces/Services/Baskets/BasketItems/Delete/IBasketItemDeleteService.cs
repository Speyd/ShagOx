using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Delete;
public interface IBasketItemDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<BasketItem, DeleteResponse>
{
}
