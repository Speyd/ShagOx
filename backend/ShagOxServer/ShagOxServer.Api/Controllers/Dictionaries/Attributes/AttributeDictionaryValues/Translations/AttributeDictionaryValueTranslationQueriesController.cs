using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Translations;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Query;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues.Translations;

namespace ShagOxServer.Api.Controllers.Dictionaries.Attributes.AttributeDictionaryValues.Translations;

[ApiController]
[Route("api/attribute-dictionary-values/translations")]
public class AttributeDictionaryValueTranslationQueriesController
    : BaseTranslationQueryController<AttributeDictionaryValueTranslationDto,
        AttributeDictionaryValueTranslationSearchFilter>
{
    public AttributeDictionaryValueTranslationQueriesController(
        IAttributeDictionaryValueTranslationQueryService queryService,
        ILanguageProvider languageProvider
    )
        : base(queryService, languageProvider)
    {
    }
}
