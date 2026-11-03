using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Translations;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Query;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues.Translations;

namespace ShagOxServer.Api.Controllers.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Admin;

[ApiController]
[Route("api/admin/attribute-dictionary-values/translations")]
[Authorize(Roles = "Admin")]
public class AttributeDictionaryValueTranslationAdminQueriesController
    : BaseTranslationAdminQueryController<AttributeDictionaryValueTranslationDto,
        AttributeDictionaryValueTranslationSearchFilter>
{
    public AttributeDictionaryValueTranslationAdminQueriesController(
        IAttributeDictionaryValueTranslationQueryService queryService,
        ILanguageProvider languageProvider
    )
        : base(queryService, languageProvider)
    {
    }
}
