using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.AttributeDefinitions.Translations;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions.Translations;
public interface IAttributeDefinitionTranslationQueryRepository
    : IQueryTranslationRepository<AttributeDefinitionTranslation>
{
    Task<PagedResult<AttributeDefinitionTranslation>> Search(
       AttributeDefinitionTranslationSearchFilter filter,
       PaginationParams pagination);
}