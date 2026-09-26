using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions.Translations;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions.Translations;
public interface IAttributeDefinitionTranslationQueryRepository
    : IQueryTranslationRepository<AttributeDefinitionTranslation>
{
    Task<PagedResult<AttributeDefinitionTranslation>> Search(
       AttributeDefinitionTranslationSearchFilter filter,
       PaginationParams pagination);
}