using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions.Translations;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Query;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Mapping;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Query;
public class AttributeDefinitionQueryService 
    : BaseTranslatableQueryService<
        AttributeDefinitionDto,
        AttributeDefinition,
        AttributeDefinitionSearchFilter
        >,
    IAttributeDefinitionQueryService
{
    public readonly IAttributeDefinitionQueryRepository _attributeRepository;

    private readonly IAttributeDefinitionTranslationQueryRepository _translationRepository;

    private readonly ILanguageProvider _language;


    public AttributeDefinitionQueryService(
        IAttributeDefinitionQueryRepository attributeQueryRepository,
        IAttributeDefinitionTranslationQueryRepository translationRepository,
        ILanguageProvider language
    )
        : base(attributeQueryRepository)
    {
        _attributeRepository = attributeQueryRepository;
        _translationRepository = translationRepository;
        _language = language;
    }


    public override async Task<AttributeDefinitionDto> ApplyMapperAsync(
        AttributeDefinition entity)
    {
        var translation = await _translationRepository
            .GetByIdentificatorAsync(entity.Key, _language.Language);

        return AttributeDefinitionMapper.ToDto(entity, translation?.Name);
    }
}