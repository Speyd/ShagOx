using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Translations;
using ShagOxServer.Application.Interfaces.Services.Base.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.AttributeDefinitions;
using ShagOxServer.Domain.Filters.Dictionaries.AttributeDefinitions.Translations;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Translations.Query;
public interface IAttributeDefinitionTranslationQueryService
    : IQueryTranslationService<AttributeDefinitionTranslationDto>
{
    Task<Result<PagedResult<AttributeDefinitionTranslationDto>>> Search(
       AttributeDefinitionTranslationSearchFilter filter,
       PaginationParams pagination);
}