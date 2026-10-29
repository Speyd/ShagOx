using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Query;
using ShagOxServer.Application.Interfaces.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Query;
public interface IAttributeDefinitionQueryService
    : ITranslatableQueryService<AttributeDefinitionDto,
        AttributeDefinition,
        AttributeDefinitionSearchFilter>
{
    Task<Result<List<AttributeDefinitionDto>>> GetByCategoryAsync(
        long categoryId,
        CancellationToken cancellationToken = default);
}
