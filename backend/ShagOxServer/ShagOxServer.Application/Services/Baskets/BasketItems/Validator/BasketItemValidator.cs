using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.LineItems.Core.Validator;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Validator;
public class BasketItemValidator
    : LineItemValidator<BasketItem>
{
    public BasketItemValidator(
        IRepository<BasketItem> repository,
        IBasketItemExistsRepository itemExistsRepository,
        IBasketItemQueryRepository itemQeuryRepository,
        IDataCacheService cache
    ) 
        : base(repository, 
            itemExistsRepository, 
            itemQeuryRepository, 
            cache)
    {
    }
}
