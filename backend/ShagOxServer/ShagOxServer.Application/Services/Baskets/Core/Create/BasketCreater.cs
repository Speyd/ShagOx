using ShagOxServer.Application.DTOs.Baskets.Core.Create;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Baskets.Core.Create;
public static class BasketCreater
{
    public static Basket Create(
       BasketCreateRequest request)
    {
        return new Basket
        {
            UserId = request.UserId
        };
    }
}