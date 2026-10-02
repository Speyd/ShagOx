using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Translations;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Mapping;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Query;
public class AttributeDictionaryValueQueryService
    : BaseTranslatableQueryService<
        AttributeDictionaryValueDto,
        AttributeDictionaryValue,
        AttributeDictionaryValueSearchFilter
        >,
    IAttributeDictionaryValueQueryService
{
    protected readonly IAttributeDictionaryValueQueryRepository 
        _dictionaryDictQueryRepository;

    protected readonly IAttributeDictionaryValueTranslationQueryRepository _translation;

    protected readonly ILanguageProvider _language;


    public AttributeDictionaryValueQueryService(
        IAttributeDictionaryValueQueryRepository dictionaryDictQueryRepository,
        IAttributeDictionaryValueTranslationQueryRepository translation,
        ILanguageProvider language
    )
        : base(dictionaryDictQueryRepository)
    {
        _dictionaryDictQueryRepository = dictionaryDictQueryRepository;
        _translation = translation;
        _language = language;
    }


    public override async Task<AttributeDictionaryValueDto> ApplyMapperAsync(
        AttributeDictionaryValue entity)
    {
        var translation = await _translation
            .GetByIdentificatorAsync(entity.Code, _language.Language);

        return AttributeDictionaryValueMapper.ToDto(entity, translation?.Name);
    }


    public async Task<Result<PagedResult<AttributeDictionaryValueDto>>> 
        GetByDictionaryAsync(
        long dictionaryId, 
        PaginationParams pagination)
    {
        var values = await _dictionaryDictQueryRepository
            .GetByDictionaryAsync(dictionaryId, pagination);

        return await values.ToResultPagedAsync(ApplyMapperAsync);
    }
}