using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Interfaces.Services.Baskets.Core.Delete;
public interface IBasketDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<Basket, DeleteResponse>
{
}
