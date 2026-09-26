using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Translations;
using ShagOxServer.Application.Interfaces.Services.Base.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions.Translations;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Query;
public interface IAttributeDefinitionTranslationQueryService
    : IQueryTranslationService<AttributeDefinitionTranslationDto>
{
    Task<Result<PagedResult<AttributeDefinitionTranslationDto>>> Search(
       AttributeDefinitionTranslationSearchFilter filter,
       PaginationParams pagination);
}