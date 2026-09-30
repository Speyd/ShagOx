using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues;
public interface IAttributeDictionaryValueExistsRepository
    : IExistsRepository<AttributeDictionaryValue>
{
    Task<bool> ExistsAsync(
        long dictionaryId,
        string code);

    Task<bool> ExistsByCodeAsync(
        string code);
}