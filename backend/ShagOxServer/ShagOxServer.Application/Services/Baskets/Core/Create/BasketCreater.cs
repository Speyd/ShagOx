using ShagOxServer.Application.DTOs.Baskets.Core.Create;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Baskets.Core.Create;
public static class BasketCreater
{
    public static Basket Create(
        long ownerId,
        BasketCreateRequest request)
    {
        
        return new Basket
        {
            OwnerId = ownerId,
            EntityStatus = EntityStatus.Active
        };
    }
}
