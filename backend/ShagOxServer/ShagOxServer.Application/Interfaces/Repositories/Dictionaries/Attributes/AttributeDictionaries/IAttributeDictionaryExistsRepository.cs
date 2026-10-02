using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaries;
public interface IAttributeDictionaryExistsRepository
    : IExistsRepository<AttributeDictionary>
{
    Task<bool> ExistsByCodeAsync(
        string code);
}