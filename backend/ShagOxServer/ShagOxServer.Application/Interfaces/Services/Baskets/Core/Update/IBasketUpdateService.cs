using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Baskets.Core.Update;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Baskets.Core.Update;
public interface IBasketUpdateService
    : IUpdateService<UpdateResponse, BasketUpdateRequest>
{
}