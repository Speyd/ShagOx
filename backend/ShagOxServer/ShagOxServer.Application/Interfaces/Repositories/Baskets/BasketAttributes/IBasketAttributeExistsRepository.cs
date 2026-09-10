using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes;
public interface IBasketAttributeExistsRepository
    : IExistsRepository<BasketAttribute>
{
    Task<bool> ExistsAsync(
        int attributeDefenitionId,
        int order);

    Task<bool> ExistsByCategoryAsync(
        int categoryId);

    Task<bool> ExistsByAttributeDefenitionAsync(
        int attributeDefenitionId);
}