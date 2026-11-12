using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.LineItems;
using System.Text.Json;

namespace ShagOxServer.Application.Interfaces.Repositories.LineItems.LineItemAttributes;
public interface ILineItemAttributeExistsRepository
    : IExistsRepository<LineItemAttribute>
{
    Task<bool> ExistsAsync(
        long categoryId,
        long attributeId,
        int order,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<bool> ExistsByCategoryAsync(
        long categoryId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<bool> ExistsByAttributeDefenitionAsync(
        long attributeDefenitionId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<List<string>> ExistsByKeysAsync(
        JsonDocument attributes,
        long categoryId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
