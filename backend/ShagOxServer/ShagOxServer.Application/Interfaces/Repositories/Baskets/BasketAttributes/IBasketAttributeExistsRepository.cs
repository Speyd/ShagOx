using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Baskets;
using System.Text.Json;

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

    Task<List<string>> ExistsByKeysAsync(
        JsonDocument attributes,
        long categoryId,
        CancellationToken cancellationToken = default);
}
