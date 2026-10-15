using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes;
public interface IBasketAttributeExistsRepository
    : IExistsRepository<BasketAttribute>
{
    Task<bool> ExistsAsync(
        long categoryId,
        long attributeId,
        int order,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByCategoryAsync(
        long categoryId,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByAttributeDefenitionAsync(
        long attributeDefenitionId,
        CancellationToken cancellationToken = default);
}
